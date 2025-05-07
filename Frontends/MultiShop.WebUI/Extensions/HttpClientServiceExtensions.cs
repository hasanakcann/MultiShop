using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;
using MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;
using MultiShop.WebUI.Services.CatalogServices.AboutServices;
using MultiShop.WebUI.Services.CatalogServices.BrandServices;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using MultiShop.WebUI.Services.CatalogServices.ContactServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;
using MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;
using MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using MultiShop.WebUI.Services.CommentServices;
using MultiShop.WebUI.Services.Concrete;
using MultiShop.WebUI.Services.DiscountServices;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Services.MessageServices;
using MultiShop.WebUI.Services.OrderServices.OrderAdressServices;
using MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;
using MultiShop.WebUI.Services.UserIdentityServices;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Extensions;

public static class HttpClientServiceExtensions
{
    public static IServiceCollection AddMultiShopIdentityHttpClients(this IServiceCollection services, ServiceApiSettings serviceApiSettings)
    {
        services.AddHttpClient<IUserService, UserService>(options =>
        {
            options.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IUserStatisticService, UserStatisticService>(options =>
        {
            options.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IUserIdentityService, UserIdentityService>(options =>
        {
            options.BaseAddress = new Uri(serviceApiSettings.IdentityServerUrl);
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        return services;
    }

    public static IServiceCollection AddMultiShopAuthenticatedHttpClients(this IServiceCollection services, ServiceApiSettings serviceApiSettings)
    {
        services.AddHttpClient<IBasketService, BasketService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Basket.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IDiscountService, DiscountService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Discount.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IOrderAdressService, OrderAdressService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Order.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IOrderOrderingService, OrderOrderingService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Order.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IMessageService, MessageService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Message.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<ICargoCompanyService, CargoCompanyService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Cargo.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<ICargoCustomerService, CargoCustomerService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Cargo.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<ICatalogStatisticService, CatalogStatisticService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<ICommentStatisticService, CommentStatisticService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Comment.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IDiscountStatisticService, DiscountStatisticService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Discount.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        services.AddHttpClient<IMessageStatisticService, MessageStatisticService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Message.Path}");
        }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

        return services;
    }

    public static IServiceCollection AddMultiShopApplicationHttpClients(this IServiceCollection services, ServiceApiSettings serviceApiSettings)
    {
        services.AddHttpClient<ICategoryService, CategoryService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IProductService, ProductService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<ISpecialOfferService, SpecialOfferService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IFeatureSliderService, FeatureSliderService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IFeatureService, FeatureService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IOfferDiscountService, OfferDiscountService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IBrandService, BrandService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IAboutService, AboutService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IProductImageService, ProductImageService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IProductDetailService, ProductDetailService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<ICommentService, CommentService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Comment.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        services.AddHttpClient<IContactService, ContactService>(options =>
        {
            options.BaseAddress = new Uri($"{serviceApiSettings.OcelotUrl}/{serviceApiSettings.Catalog.Path}");
        }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

        return services;
    }
}