using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;

namespace MultiShop.WebUI.Services.CatalogServices.FeatureServices;

public class FeatureService : IFeatureService
{
    private readonly HttpClient _httpClient;

    public FeatureService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
    {
        if (createFeatureDto == null) throw new ArgumentNullException(nameof(createFeatureDto));

        try
        {
            var response = await _httpClient.PostAsJsonAsync("features", createFeatureDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to create feature.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the feature.", ex);
        }
    }

    public async Task DeleteFeatureAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.DeleteAsync($"features?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to delete feature.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the feature.", ex);
        }
    }

    public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("features");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to retrieve features.");

            var features = await response.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
            return features ?? new List<ResultFeatureDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving features.", ex);
        }
    }

    public async Task<UpdateFeatureDto> GetByIdFeatureAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.GetAsync($"features/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get feature with ID {id}.");

            var feature = await response.Content.ReadFromJsonAsync<UpdateFeatureDto>();
            return feature ?? throw new ApplicationException("Feature not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the feature by ID.", ex);
        }
    }

    public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
    {
        if (updateFeatureDto == null) throw new ArgumentNullException(nameof(updateFeatureDto));

        try
        {
            var response = await _httpClient.PutAsJsonAsync("features", updateFeatureDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to update feature.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the feature.", ex);
        }
    }
}
