using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete;

public class IdentityService : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClientSettings _clientSettings;
    private readonly ServiceApiSettings _serviceApiSettings;

    public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor,
        IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _clientSettings = clientSettings.Value ?? throw new ArgumentNullException(nameof(clientSettings));
        _serviceApiSettings = serviceApiSettings.Value ?? throw new ArgumentNullException(nameof(serviceApiSettings));
    }

    public async Task<bool> SignIn(SignInDto signInDto)
    {
        try
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
                throw new ApplicationException($"Discovery endpoint error: {discovery.Error}");

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = signInDto.UserName,
                Password = signInDto.Password,
                Address = discovery.TokenEndpoint
            });

            if (tokenResponse.IsError)
                throw new ApplicationException($"Token request failed: {tokenResponse.Error}");

            var userInfo = await _httpClient.GetUserInfoAsync(new UserInfoRequest
            {
                Token = tokenResponse.AccessToken,
                Address = discovery.UserInfoEndpoint
            });

            if (userInfo.IsError)
                throw new ApplicationException($"User info request failed: {userInfo.Error}");

            var claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddSeconds(tokenResponse.ExpiresIn)
            };

            authProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = tokenResponse.AccessToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = tokenResponse.RefreshToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o") }
            });

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred during sign-in process.", ex);
        }
    }

    public async Task<bool> GetRefreshToken()
    {
        try
        {
            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
                throw new ApplicationException($"Discovery endpoint error: {discovery.Error}");

            var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
            if (string.IsNullOrWhiteSpace(refreshToken))
                throw new ApplicationException("Refresh token not found.");

            var tokenResponse = await _httpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                RefreshToken = refreshToken,
                Address = discovery.TokenEndpoint
            });

            if (tokenResponse.IsError)
                throw new ApplicationException($"Token refresh failed: {tokenResponse.Error}");

            var result = await _httpContextAccessor.HttpContext.AuthenticateAsync();
            var properties = result.Properties;

            properties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = OpenIdConnectParameterNames.AccessToken, Value = tokenResponse.AccessToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.RefreshToken, Value = tokenResponse.RefreshToken },
                new AuthenticationToken { Name = OpenIdConnectParameterNames.ExpiresIn, Value = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn).ToString("o") }
            });

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while refreshing the token.", ex);
        }
    }
}
