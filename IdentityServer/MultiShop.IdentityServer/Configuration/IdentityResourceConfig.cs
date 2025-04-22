using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer.Configuration;

public static class IdentityResourceConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources() => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Email(),
        new IdentityResources.Profile()
    };
}