using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers;

public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdentityService _identityService;
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService identityService)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext is null)
            throw new InvalidOperationException("HttpContext cannot be null.");

        var accessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new InvalidOperationException("Access token is missing from the current context.");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                var newAccessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                if (accessToken != newAccessToken)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                    return await base.SendAsync(request, cancellationToken);
                }

                var isTokenRefreshed = await _identityService.GetRefreshToken();
                if (!isTokenRefreshed)
                    throw new ApplicationException("Token refresh failed. Unauthorized access.");

                newAccessToken = await httpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                if (string.IsNullOrWhiteSpace(newAccessToken))
                    throw new ApplicationException("Access token could not be retrieved after refresh.");

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        return response;
    }
}
