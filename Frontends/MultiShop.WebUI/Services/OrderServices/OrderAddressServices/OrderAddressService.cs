using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices;

public class OrderAddressService : IOrderAddressService
{
    private readonly HttpClient _httpClient;

    public OrderAddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto)
    {
        if (createOrderAddressDto is null)
            throw new ArgumentNullException(nameof(createOrderAddressDto));

        var response = await _httpClient.PostAsJsonAsync("addresses", createOrderAddressDto);
        response.EnsureSuccessStatusCode();
    }
}
