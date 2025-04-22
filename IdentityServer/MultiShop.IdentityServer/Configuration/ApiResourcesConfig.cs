using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer.Configuration;

public static class ApiResourcesConfig
{
    public static IEnumerable<ApiResource> GetApiResources() => new ApiResource[]
    {
      new ApiResource("ResourceBasket")   { Scopes = { "BasketFullPermission" } },
      new ApiResource("ResourceCargo")    { Scopes = { "CargoFullPermission" } },
      new ApiResource("ResourceCatalog")  { Scopes = { "CatalogFullPermission", "CatalogReadPermission" } },
      new ApiResource("ResourceComment")  { Scopes = { "CommentFullPermission" } },
      new ApiResource("ResourceDiscount") { Scopes = { "DiscountFullPermission" } },
      new ApiResource("ResourceImage")    { Scopes = { "ImageFullPermission" } },
      new ApiResource("ResourceMessage")  { Scopes = { "MessageFullPermission" } },
      new ApiResource("ResourceOcelot")   { Scopes = { "OcelotFullPermission" } },
      new ApiResource("ResourceOrder")    { Scopes = { "OrderFullPermission" } },
      new ApiResource("ResourcePayment")  { Scopes = { "PaymentFullPermission" } },
      new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };
}
