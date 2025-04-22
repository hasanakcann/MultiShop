using IdentityServer4.Models;
using MultiShop.IdentityServer.Configuration;
using System.Collections.Generic;

namespace MultiShop.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources => IdentityResourceConfig.GetIdentityResources();
    public static IEnumerable<ApiResource> ApiResources => ApiResourcesConfig.GetApiResources();
    public static IEnumerable<ApiScope> ApiScopes => ApiScopesConfig.GetApiScopes();
    public static IEnumerable<Client> Clients => ClientConfig.GetClients();
}