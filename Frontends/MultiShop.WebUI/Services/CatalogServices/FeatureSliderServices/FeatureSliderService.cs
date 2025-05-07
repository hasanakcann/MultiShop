using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureSliderServices;

public class FeatureSliderService : IFeatureSliderService
{
    private readonly HttpClient _httpClient;

    public FeatureSliderService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
    {
        try
        {
            if (createFeatureSliderDto == null) throw new ArgumentNullException(nameof(createFeatureSliderDto));
            await _httpClient.PostAsJsonAsync("featuresliders", createFeatureSliderDto);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the feature slider.", ex);
        }
    }

    public async Task DeleteFeatureSliderAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));
            await _httpClient.DeleteAsync($"featuresliders?id={id}");
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the feature slider.", ex);
        }
    }

    public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("featuresliders");

            if (!responseMessage.IsSuccessStatusCode)
                return new List<ResultFeatureSliderDto>();

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(jsonData);
            return values ?? new List<ResultFeatureSliderDto>();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving feature sliders.", ex);
        }
    }

    public async Task<UpdateFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException(nameof(id));

            var responseMessage = await _httpClient.GetAsync($"featuresliders/{id}");

            if (!responseMessage.IsSuccessStatusCode)
                return null;

            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateFeatureSliderDto>();
            return values;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving the feature slider by ID.", ex);
        }
    }

    public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
    {
        try
        {
            if (updateFeatureSliderDto == null) throw new ArgumentNullException(nameof(updateFeatureSliderDto));
            await _httpClient.PutAsJsonAsync("featuresliders", updateFeatureSliderDto);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the feature slider.", ex);
        }
    }

    public Task FeatureSliderChangeStatusToFalse(string id)
    {
        throw new NotImplementedException("FeatureSliderChangeStatusToFalse method is not implemented yet.");
    }

    public Task FeatureSliderChangeStatusToTrue(string id)
    {
        throw new NotImplementedException("FeatureSliderChangeStatusToTrue method is not implemented yet.");
    }
}
