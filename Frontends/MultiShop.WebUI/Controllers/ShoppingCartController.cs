using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.BasketDtos;
using MultiShop.WebUI.Services.BasketServices;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Controllers;

public class ShoppingCartController : Controller
{
    private readonly IProductService _productService;
    private readonly IBasketService _basketService;
    public ShoppingCartController(IProductService productService, IBasketService basketService)
    {
        _productService = productService;
        _basketService = basketService;
    }

    public async Task<IActionResult> Index(string code, int discountRate, decimal totalNewPriceWithDiscount)
    {
        ViewBag.code = code;
        ViewBag.discountRate = discountRate;
        ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

        ViewBag.directory1 = "Ana Sayfa";
        ViewBag.directory2 = "Ürünler";
        ViewBag.directory3 = "Sepetim";

        var basket = await _basketService.GetBasket();

        ViewBag.total = basket.TotalPrice;

        var tax = basket.TotalPrice * 0.20m;
        var totalPriceWithTax = basket.TotalPrice + tax;

        ViewBag.tax = tax;
        ViewBag.totalPriceWithTax = totalPriceWithTax;

        return View();
    }

    public async Task<IActionResult> AddBasketItem(string id)
    {
        var product = await _productService.GetByIdProductAsync(id);
        var basketItems = new BasketItemDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Price = product.ProductPrice,
            Quantity = 1,
            ProductImageUrl = product.ProductImageUrl
        };
        await _basketService.AddBasketItem(basketItems);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemoveBasketItem(string id)
    {
        await _basketService.RemoveBasketItem(id);
        return RedirectToAction("Index");
    }
}
