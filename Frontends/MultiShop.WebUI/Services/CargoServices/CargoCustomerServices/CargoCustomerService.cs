using MultiShop.DtoLayer.CargoDtos.CargoCustomerDtos;

namespace MultiShop.WebUI.Services.CargoServices.CargoCustomerServices;

public class CargoCustomerService : ICargoCustomerService
{
    private readonly HttpClient _httpClient;

    public CargoCustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient cannot be null.");
    }

    public async Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentException("Cargo customer ID cannot be null or empty.", nameof(id));
        var response = await _httpClient.GetAsync($"cargocustomers/getcargocustomerbyuserid/{id}");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch cargo customer with ID {id}. Status Code: {response.StatusCode}");

        var cargoCustomer = await response.Content.ReadFromJsonAsync<GetCargoCustomerByIdDto>();
        if (cargoCustomer == null)
            throw new InvalidOperationException($"Cargo customer with ID {id} was not found.");

        return cargoCustomer;
    }
}
