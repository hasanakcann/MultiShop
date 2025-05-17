using System.Text.Json;

namespace MultiShop.WebUI.Services.StatisticServices.DiscountStatisticServices;

public class DiscountStatisticService : IDiscountStatisticService
{
    private readonly HttpClient _httpClient;
    public DiscountStatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<int> GetDiscountCouponCountAsync()
    {
        var responseMessage = await _httpClient.GetAsync("discounts/getdiscountcouponcount");
        using var stream = await responseMessage.Content.ReadAsStreamAsync();
        using var jsonDoc = await JsonDocument.ParseAsync(stream);

        if (jsonDoc.RootElement.TryGetProperty("count", out JsonElement countElement) && countElement.TryGetInt32(out int count))
        {
            return count;
        }
        return 0;
    }
}
