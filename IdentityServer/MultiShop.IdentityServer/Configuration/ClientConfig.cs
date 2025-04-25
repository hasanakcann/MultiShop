using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer.Configuration;

public static class ClientConfig
{
    private static Secret DefaultSecret => new Secret("multishopsecret".Sha256());

    public static IEnumerable<Client> GetClients() => new Client[]
    {
        new Client
        {
            ClientId = "MultiShopVisitorId",
            ClientName = "Multi Shop Visitor User",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { DefaultSecret },
            AllowedScopes =
            {
                "CatalogFullPermission",
                "CatalogReadPermission",
                "CommentFullPermission",
                "ImageFullPermission",
                "OcelotFullPermission"
            }
        },
        new Client
        {
            ClientId = "MultiShopManagerId",
            ClientName = "Multi Shop Manager User",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { DefaultSecret },
            AllowedScopes =
            {
                "BasketFullPermission",
                "CargoFullPermission",
                "CatalogFullPermission",
                "CatalogReadPermission",
                "CommentFullPermission",
                "DiscountFullPermission",
                "ImageFullPermission",
                "MessageFullPermission",
                "OcelotFullPermission",
                "OrderFullPermission",
                "PaymentFullPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Profile
            }
        },
        new Client
        {
            ClientId = "MultiShopAdminId",
            ClientName = "Multi Shop Admin User",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { DefaultSecret },
            AllowedScopes =
            {
                "BasketFullPermission",
                "CargoFullPermission",
                "CatalogFullPermission",
                "CatalogReadPermission",
                "CommentFullPermission",
                "DiscountFullPermission",
                "ImageFullPermission",
                "MessageFullPermission",
                "OcelotFullPermission",
                "OrderFullPermission",
                "PaymentFullPermission",
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.Profile
            },
            AccessTokenLifetime = 600
        }
    };
}