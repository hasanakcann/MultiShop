using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddMultiShopGeneralServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddAccessTokenManagement();
        services.AddHttpContextAccessor();

        services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
        services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));

        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ResourceOwnerPasswordTokenHandler>();
        services.AddScoped<ClientCredentialTokenHandler>();
        services.AddHttpClient<IIdentityService, IdentityService>();
        services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

        return services;
    }
}