using MultiShop.DtoLayer.CatalogDtos.AboutDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.AboutServices;

public class AboutService : IAboutService
{
    private readonly HttpClient _httpClient;

    public AboutService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
    {
        try
        {
            await _httpClient.PostAsJsonAsync("abouts", createAboutDto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating about data.", ex);
        }
    }

    public async Task DeleteAboutAsync(string id)
    {
        try
        {
            await _httpClient.DeleteAsync($"abouts?id={id}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting about data.", ex);
        }
    }

    public async Task<List<ResultAboutDto>> GetAllAboutAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("abouts");
            responseMessage.EnsureSuccessStatusCode();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData);
            return values ?? new List<ResultAboutDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the about list.", ex);
        }
    }

    public async Task<UpdateAboutDto> GetByIdAboutAsync(string id)
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync($"abouts/{id}");
            responseMessage.EnsureSuccessStatusCode();

            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateAboutDto>();
            return values!;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving about details.", ex);
        }
    }

    public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
    {
        try
        {
            await _httpClient.PutAsJsonAsync("abouts", updateAboutDto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating about data.", ex);
        }
    }
}
