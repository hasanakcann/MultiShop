using MultiShop.DtoLayer.CargoDtos.CargoCompanyDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CargoServices.CargoCompanyServices;

public class CargoCompanyService : ICargoCompanyService
{
    private readonly HttpClient _httpClient;

    public CargoCompanyService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient cannot be null.");
    }

    public async Task CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto)
    {
        if (createCargoCompanyDto == null)
            throw new ArgumentNullException(nameof(createCargoCompanyDto), "CreateCargoCompanyDto cannot be null.");

        var response = await _httpClient.PostAsJsonAsync("cargocompanies", createCargoCompanyDto);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to create cargo company. Status Code: {response.StatusCode}");
    }

    public async Task DeleteCargoCompanyAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid cargo company ID.", nameof(id));
        var response = await _httpClient.DeleteAsync($"cargocompanies/{id}");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to delete cargo company with ID {id}. Status Code: {response.StatusCode}");
    }

    public async Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync()
    {
        var response = await _httpClient.GetAsync("cargocompanies");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch cargo companies. Status Code: {response.StatusCode}");

        var jsonData = await response.Content.ReadAsStringAsync();
        var cargoCompanyList = JsonConvert.DeserializeObject<List<ResultCargoCompanyDto>>(jsonData);

        return cargoCompanyList ?? new List<ResultCargoCompanyDto>();
    }

    public async Task<UpdateCargoCompanyDto> GetByIdCargoCompanyAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("Invalid cargo company ID.", nameof(id));

        var response = await _httpClient.GetAsync($"cargocompanies/{id}");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch cargo company with ID {id}. Status Code: {response.StatusCode}");

        var cargoCompany = await response.Content.ReadFromJsonAsync<UpdateCargoCompanyDto>();
        if (cargoCompany == null)
            throw new InvalidOperationException($"Cargo company with ID {id} was not found.");

        return cargoCompany;
    }

    public async Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto)
    {
        if (updateCargoCompanyDto == null)
            throw new ArgumentNullException(nameof(updateCargoCompanyDto), "UpdateCargoCompanyDto cannot be null.");

        var response = await _httpClient.PutAsJsonAsync("cargocompanies", updateCargoCompanyDto);
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to update cargo company. Status Code: {response.StatusCode}");
    }
}
