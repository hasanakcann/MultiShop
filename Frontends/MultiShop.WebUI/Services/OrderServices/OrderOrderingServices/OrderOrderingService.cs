using MultiShop.DtoLayer.OrderDtos.OrderOrderingDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.OrderServices.OrderOrderingServices;

public class OrderOrderingService : IOrderOrderingService
{
    private readonly HttpClient _httpClient;

    public OrderOrderingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return new List<ResultOrderingByUserIdDto>();

        var responseMessage = await _httpClient.GetAsync($"orderings/getorderingbyuserid/{id}");

        if (!responseMessage.IsSuccessStatusCode)
            return new List<ResultOrderingByUserIdDto>();

        var jsonData = await responseMessage.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(jsonData))
            return new List<ResultOrderingByUserIdDto>();

        var order = JsonConvert.DeserializeObject<List<ResultOrderingByUserIdDto>>(jsonData);

        return order ?? new List<ResultOrderingByUserIdDto>();
    }
}
