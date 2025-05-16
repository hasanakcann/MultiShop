using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/ProductImage")]
public class ProductImageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private const string baseUrl = "http://localhost:7070/api/ProductImages";

    public ProductImageController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("ProductImageDetail/{id}")]
    [HttpGet]
    public async Task<IActionResult> ProductImageDetail(string id)
    {
        ViewBag.v0 = "Ürün Görsel İşlemleri";
        ViewBag.v1 = "Ana Sayfa";
        ViewBag.v2 = "Ürünler";
        ViewBag.v3 = "Ürün Görsel Güncelleme Sayfası";

        var client = _httpClientFactory.CreateClient();

        var responseMessage = await client.GetAsync($"{baseUrl}/ProductImagesByProductId/{id}");

        if (responseMessage.IsSuccessStatusCode)
        {
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var productImage = JsonConvert.DeserializeObject<UpdateProductImageDto>(jsonData);
            return View(productImage);
        }

        return View();
    }

    [Route("ProductImageDetail/{id}")]
    [HttpPost]
    public async Task<IActionResult> ProductImageDetail(UpdateProductImageDto updateProductImageDto)
    {
        var client = _httpClientFactory.CreateClient();

        var jsonData = JsonConvert.SerializeObject(updateProductImageDto);
        var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

        var responseMessage = await client.PutAsync(baseUrl, stringContent);

        if (responseMessage.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductListWithCategory", "Product", new { area = "Admin" });
        }

        return View(updateProductImageDto);
    }
}
