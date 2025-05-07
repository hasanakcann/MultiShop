using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductServices;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateProductAsync(CreateProductDto createProductDto)
    {
        if (createProductDto == null)
        {
            throw new ArgumentNullException(nameof(createProductDto), "Product detail cannot be null.");
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("products", createProductDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the product.", ex);
        }
    }

    public async Task DeleteProductAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Product ID cannot be null or empty.");
        }

        try
        {
            var response = await _httpClient.DeleteAsync($"products?id={id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the product.", ex);
        }
    }

    public async Task<List<ResultProductDto>> GetAllProductAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("products");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);

            return values ?? new List<ResultProductDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all products.", ex);
        }
    }

    public async Task<UpdateProductDto> GetByIdProductAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Product ID cannot be null or empty.");
        }

        try
        {
            var responseMessage = await _httpClient.GetAsync($"products/{id}");
            responseMessage.EnsureSuccessStatusCode();
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();

            return values ?? new UpdateProductDto();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product by ID.", ex);
        }
    }

    public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
    {
        if (updateProductDto == null)
        {
            throw new ArgumentNullException(nameof(updateProductDto), "Product detail cannot be null.");
        }

        try
        {
            var response = await _httpClient.PutAsJsonAsync("products", updateProductDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the product.", ex);
        }
    }

    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("products/productlistwithcategory");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductsWithCategoryDto>>(jsonData);

            return values ?? new List<ResultProductsWithCategoryDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving products with categories.", ex);
        }
    }

    public async Task<List<ResultProductsWithCategoryDto>> GetProductsWithCategoryByCategoryIdAsync(string categoryId)
    {
        if (string.IsNullOrEmpty(categoryId))
        {
            throw new ArgumentNullException(nameof(categoryId), "Category ID cannot be null or empty.");
        }

        try
        {
            var responseMessage = await _httpClient.GetAsync($"products/productswithcategorybycategoryid/{categoryId}");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductsWithCategoryDto>>(jsonData);

            return values ?? new List<ResultProductsWithCategoryDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving products with categories by category ID.", ex);
        }
    }
}
