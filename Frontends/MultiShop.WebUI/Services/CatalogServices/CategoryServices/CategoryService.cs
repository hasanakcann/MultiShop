using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;

namespace MultiShop.WebUI.Services.CatalogServices.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
    {
        if (createCategoryDto == null) throw new ArgumentNullException(nameof(createCategoryDto));

        try
        {
            var response = await _httpClient.PostAsJsonAsync("categories", createCategoryDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to create category.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the category.", ex);
        }
    }

    public async Task DeleteCategoryAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.DeleteAsync($"categories?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to delete category.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the category.", ex);
        }
    }

    public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to retrieve categories.");

            var categories = await response.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return categories ?? new List<ResultCategoryDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving categories.", ex);
        }
    }

    public async Task<UpdateCategoryDto> GetByIdCategoryAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.GetAsync($"categories/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get category with ID {id}.");

            var category = await response.Content.ReadFromJsonAsync<UpdateCategoryDto>();
            return category ?? throw new ApplicationException("Category not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the category by ID.", ex);
        }
    }

    public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
    {
        if (updateCategoryDto == null) throw new ArgumentNullException(nameof(updateCategoryDto));

        try
        {
            var response = await _httpClient.PutAsJsonAsync("categories", updateCategoryDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to update category.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the category.", ex);
        }
    }
}
