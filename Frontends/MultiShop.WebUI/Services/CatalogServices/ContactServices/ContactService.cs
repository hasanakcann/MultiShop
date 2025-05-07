using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices;

public class ContactService : IContactService
{
    private readonly HttpClient _httpClient;

    public ContactService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        if (createContactDto == null) throw new ArgumentNullException(nameof(createContactDto));

        try
        {
            var response = await _httpClient.PostAsJsonAsync("contacts", createContactDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to create contact.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the contact.", ex);
        }
    }

    public async Task DeleteContactAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.DeleteAsync($"contacts?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to delete contact.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the contact.", ex);
        }
    }

    public async Task<List<ResultContactDto>> GetAllContactAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("contacts");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to retrieve contacts.");

            var contacts = await response.Content.ReadFromJsonAsync<List<ResultContactDto>>();
            return contacts ?? new List<ResultContactDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving contacts.", ex);
        }
    }

    public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.GetAsync($"contacts/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get contact with ID {id}.");

            var contact = await response.Content.ReadFromJsonAsync<GetByIdContactDto>();
            return contact ?? throw new ApplicationException("Contact not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the contact by ID.", ex);
        }
    }

    public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
    {
        if (updateContactDto == null) throw new ArgumentNullException(nameof(updateContactDto));

        try
        {
            var response = await _httpClient.PutAsJsonAsync("contacts", updateContactDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to update contact.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the contact.", ex);
        }
    }
}
