using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MultiShop.WebUI.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddMultiShopAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Login/Index/";
                options.LogoutPath = "/Login/LogOut/";
                options.AccessDeniedPath = "/Pages/AccessDenied/";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.Cookie.Name = "MultiShopJwt";
            });

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/Login/Index/";
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.Name = "MultiShopCookie";
                options.SlidingExpiration = true;
            });

        return services;
    }
}