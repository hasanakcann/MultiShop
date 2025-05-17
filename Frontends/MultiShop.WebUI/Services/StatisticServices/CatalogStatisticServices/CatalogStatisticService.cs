namespace MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;

public class CatalogStatisticService : ICatalogStatisticService
{
    private readonly HttpClient _httpClient;
    public CatalogStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<long> GetBrandCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getbrandcount");
        var brandCount = await responseMessage.Content.ReadFromJsonAsync<long>();
        return brandCount;
    }

    public async Task<long> GetCategoryCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getcategorycount");
        var categoryCount = await responseMessage.Content.ReadFromJsonAsync<long>();
        return categoryCount;
    }

    public async Task<string> GetMaxPriceProductName()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getmaxpriceproductname");
        var maxPriceProductName = await responseMessage.Content.ReadAsStringAsync();
        return maxPriceProductName;
    }

    public async Task<string> GetMinPriceProductName()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getminpriceproductname");
        var minPriceProductName = await responseMessage.Content.ReadAsStringAsync();
        return minPriceProductName;
    }

    public async Task<decimal> GetProductAveragePrice()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getproductaverageprice");
        var averagePrice = await responseMessage.Content.ReadFromJsonAsync<decimal>();
        return averagePrice;
    }

    public async Task<long> GetProductCount()
    {
        var responseMessage = await _httpClient.GetAsync("statistics/getproductcount");
        var productCount = await responseMessage.Content.ReadFromJsonAsync<long>();
        return productCount;
    }
}
