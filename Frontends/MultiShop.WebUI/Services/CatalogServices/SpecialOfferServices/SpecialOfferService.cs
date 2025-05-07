using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;

public class SpecialOfferService : ISpecialOfferService
{
    private readonly HttpClient _httpClient;

    public SpecialOfferService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
    {
        if (createSpecialOfferDto == null)
        {
            throw new ArgumentNullException(nameof(createSpecialOfferDto), "Special offer data cannot be null.");
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("specialoffers", createSpecialOfferDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the special offer.", ex);
        }
    }

    public async Task DeleteSpecialOfferAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Special offer ID cannot be null or empty.");
        }

        try
        {
            var response = await _httpClient.DeleteAsync($"specialoffers?id={id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the special offer.", ex);
        }
    }

    public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("specialoffers");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(jsonData);

            return values ?? new List<ResultSpecialOfferDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all special offers.", ex);
        }
    }

    public async Task<UpdateSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Special offer ID cannot be null or empty.");
        }

        try
        {
            var responseMessage = await _httpClient.GetAsync($"specialoffers/{id}");
            responseMessage.EnsureSuccessStatusCode();
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateSpecialOfferDto>();

            return values ?? new UpdateSpecialOfferDto();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the special offer by ID.", ex);
        }
    }

    public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
    {
        if (updateSpecialOfferDto == null)
        {
            throw new ArgumentNullException(nameof(updateSpecialOfferDto), "Special offer data cannot be null.");
        }

        try
        {
            var response = await _httpClient.PutAsJsonAsync("specialoffers", updateSpecialOfferDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the special offer.", ex);
        }
    }
}
