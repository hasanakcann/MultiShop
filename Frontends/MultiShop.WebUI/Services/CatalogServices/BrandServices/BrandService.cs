using MultiShop.DtoLayer.CatalogDtos.BrandDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.BrandServices;

public class BrandService : IBrandService
{
    private readonly HttpClient _httpClient;
    public BrandService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
    {
        try
        {
            await _httpClient.PostAsJsonAsync("brands", createBrandDto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the brand.", ex);
        }
    }

    public async Task DeleteBrandAsync(string id)
    {
        try
        {
            await _httpClient.DeleteAsync($"brands?id={id}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the brand.", ex);
        }
    }

    public async Task<List<ResultBrandDto>> GetAllBrandAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("brands");
            responseMessage.EnsureSuccessStatusCode();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);
            return values ?? new List<ResultBrandDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the brand list.", ex);
        }
    }

    public async Task<UpdateBrandDto> GetByIdBrandAsync(string id)
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync($"brands/{id}");
            responseMessage.EnsureSuccessStatusCode();

            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>();
            return values!;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving brand details.", ex);
        }
    }

    public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
    {
        try
        {
            await _httpClient.PutAsJsonAsync("brands", updateBrandDto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the brand.", ex);
        }
    }
}
