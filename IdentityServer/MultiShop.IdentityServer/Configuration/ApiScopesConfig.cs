using IdentityServer4.Models;
using IdentityServer4;
using System.Collections.Generic;

namespace MultiShop.IdentityServer.Configuration;

public static class ApiScopesConfig
{
    public static IEnumerable<ApiScope> GetApiScopes() => new ApiScope[]
    {
        new ApiScope("BasketFullPermission",   "Full authority for basket operations"),
        new ApiScope("CargoFullPermission",    "Full authority for cargo operations"),
        new ApiScope("CatalogFullPermission",  "Full authority for catalog operations"),
        new ApiScope("CatalogReadPermission",  "Read-only authority for catalog operations"),
        new ApiScope("CommentFullPermission",  "Full authority for comment operations"),
        new ApiScope("DiscountFullPermission", "Full authority for discount operations"),
        new ApiScope("ImageFullPermission",    "Full authority for image operations"),
        new ApiScope("MessageFullPermission",  "Full authority for message operations"),
        new ApiScope("OcelotFullPermission",   "Full authority for ocelot operations"),
        new ApiScope("OrderFullPermission",    "Full authority for order operations"),
        new ApiScope("PaymentFullPermission",  "Full authority for payment operations"),
        new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
    };
}
