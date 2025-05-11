using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices;

public class DiscountService : IDiscountService
{
    private readonly HttpClient _httpClient;
    private const string DiscountApiBaseUrl = "http://localhost:7071/api/discounts/";

    public DiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetDiscountCodeDetailByCode> GetDiscountCodeAsync(string discountCode)
    {
        var requestUrl = $"{DiscountApiBaseUrl}GetCodeDetailByCode?code={discountCode}";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to retrieve discount code details. Status code: {response.StatusCode}");
        }

        var discountDetail = await response.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCode>();

        if (discountDetail == null)
        {
            throw new InvalidOperationException("Discount code details could not be parsed from the response.");
        }

        return discountDetail;
    }

    public async Task<int> GetDiscountCouponRateAsync(string discountCode)
    {
        var requestUrl = $"{DiscountApiBaseUrl}GetDiscountCouponRate?code={discountCode}";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Failed to retrieve discount rate. Status code: {response.StatusCode}");
        }

        var discountRate = await response.Content.ReadFromJsonAsync<int?>();

        if (discountRate == null)
        {
            throw new InvalidOperationException("Discount rate could not be parsed from the response.");
        }

        return discountRate.Value;
    }
}
