using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.WebUI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Controllers;

public class TestController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICategoryService _categoryService;
    private const string TokenEndpoint = "http://localhost:5001/connect/token";
    private const string CategoryApiUrl = "http://localhost:7070/api/Categories";

    public TestController(IHttpClientFactory httpClientFactory, ICategoryService categoryService)
    {
        _httpClientFactory = httpClientFactory;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var token = await GetAccessTokenAsync();

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Access token could not be retrieved.");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync(CategoryApiUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"API returned error: {response.StatusCode}");

            var json = await response.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(json);

            return View(categories);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"An error occurred while loading data: {ex.Message}";
            return View(new List<ResultCategoryDto>());
        }
    }

    public IActionResult Deneme1()
    {
        return View();
    }

    public async Task<IActionResult> Deneme2()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoryAsync();
            return View(categories);
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Failed to load categories: {ex.Message}";
            return View(new List<ResultCategoryDto>());
        }
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        try
        {
            using var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, TokenEndpoint)
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "client_id", "MultiShopVisitorId" },
                    { "client_secret", "multishopsecret" },
                    { "grant_type", "client_credentials" }
                })
            };

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JObject.Parse(content);

            return tokenResponse["access_token"]?.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to retrieve access token.", ex);
        }
    }
}
