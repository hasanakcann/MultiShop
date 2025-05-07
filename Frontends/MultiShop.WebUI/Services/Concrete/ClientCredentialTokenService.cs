using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete;

public class ClientCredentialTokenService : IClientCredentialTokenService
{
    private readonly ServiceApiSettings _serviceApiSettings;
    private readonly HttpClient _httpClient;
    private readonly IClientAccessTokenCache _clientAccessTokenCache;
    private readonly ClientSettings _clientSettings;

    public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache, IOptions<ClientSettings> clientSettings)
    {
        _serviceApiSettings = serviceApiSettings?.Value ?? throw new ArgumentNullException(nameof(serviceApiSettings));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _clientAccessTokenCache = clientAccessTokenCache ?? throw new ArgumentNullException(nameof(clientAccessTokenCache));
        _clientSettings = clientSettings?.Value ?? throw new ArgumentNullException(nameof(clientSettings));
    }

    public async Task<string> GetToken()
    {
        try
        {
            var cachedToken = await _clientAccessTokenCache.GetAsync("multishoptoken");
            if (cachedToken != null && !string.IsNullOrWhiteSpace(cachedToken.AccessToken))
            {
                return cachedToken.AccessToken;
            }

            var discovery = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (discovery.IsError)
            {
                throw new ApplicationException($"Discovery endpoint error: {discovery.Error}");
            }

            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.MultiShopVisitorClient.ClientId,
                ClientSecret = _clientSettings.MultiShopVisitorClient.ClientSecret,
                Address = discovery.TokenEndpoint
            });

            if (tokenResponse.IsError)
            {
                throw new ApplicationException($"Token request failed: {tokenResponse.Error}");
            }

            await _clientAccessTokenCache.SetAsync("multishoptoken", tokenResponse.AccessToken, tokenResponse.ExpiresIn);

            return tokenResponse.AccessToken;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the client credentials token.", ex);
        }
    }
}
