using MultiShop.DtoLayer.CatalogDtos.OfferDiscountDtos;

namespace MultiShop.WebUI.Services.CatalogServices.OfferDiscountServices;

public class OfferDiscountService : IOfferDiscountService
{
    private readonly HttpClient _httpClient;

    public OfferDiscountService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateOfferDiscountAsync(CreateOfferDiscountDto createOfferDiscountDto)
    {
        if (createOfferDiscountDto == null) throw new ArgumentNullException(nameof(createOfferDiscountDto));

        try
        {
            var response = await _httpClient.PostAsJsonAsync("offerdiscounts", createOfferDiscountDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to create offer discount.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the offer discount.", ex);
        }
    }

    public async Task DeleteOfferDiscountAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.DeleteAsync($"offerdiscounts?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to delete offer discount.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the offer discount.", ex);
        }
    }

    public async Task<List<ResultOfferDiscountDto>> GetAllOfferDiscountAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("offerdiscounts");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to retrieve offer discounts.");

            var offerDiscounts = await response.Content.ReadFromJsonAsync<List<ResultOfferDiscountDto>>();
            return offerDiscounts ?? new List<ResultOfferDiscountDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving offer discounts.", ex);
        }
    }

    public async Task<UpdateOfferDiscountDto> GetByIdOfferDiscountAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.GetAsync($"offerdiscounts/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get offer discount with ID {id}.");

            var offerDiscount = await response.Content.ReadFromJsonAsync<UpdateOfferDiscountDto>();
            return offerDiscount ?? throw new ApplicationException("Offer discount not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the offer discount by ID.", ex);
        }
    }

    public async Task UpdateOfferDiscountAsync(UpdateOfferDiscountDto updateOfferDiscountDto)
    {
        if (updateOfferDiscountDto == null) throw new ArgumentNullException(nameof(updateOfferDiscountDto));

        try
        {
            var response = await _httpClient.PutAsJsonAsync("offerdiscounts", updateOfferDiscountDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to update offer discount.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the offer discount.", ex);
        }
    }
}
