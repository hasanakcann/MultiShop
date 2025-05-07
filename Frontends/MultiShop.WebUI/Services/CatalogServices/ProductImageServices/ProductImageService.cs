using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ProductImageServices;

public class ProductImageService : IProductImageService
{
    private readonly HttpClient _httpClient;

    public ProductImageService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
    {
        if (createProductImageDto == null) throw new ArgumentNullException(nameof(createProductImageDto));

        try
        {
            var response = await _httpClient.PostAsJsonAsync("productimages", createProductImageDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to create product image.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the product image.", ex);
        }
    }

    public async Task DeleteProductImageAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.DeleteAsync($"productimages?id={id}");
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to delete product image.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the product image.", ex);
        }
    }

    public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("productimages");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to retrieve product images.");

            var productImages = await response.Content.ReadFromJsonAsync<List<ResultProductImageDto>>();
            return productImages ?? new List<ResultProductImageDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving product images.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var response = await _httpClient.GetAsync($"productimages/{id}");

            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get product image with ID {id}.");

            var productImage = await response.Content.ReadFromJsonAsync<GetByIdProductImageDto>();
            return productImage ?? throw new ApplicationException("Product image not found.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product image by ID.", ex);
        }
    }

    public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
    {
        if (updateProductImageDto == null) throw new ArgumentNullException(nameof(updateProductImageDto));

        try
        {
            var response = await _httpClient.PutAsJsonAsync("productimages", updateProductImageDto);
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException("Failed to update product image.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the product image.", ex);
        }
    }

    public async Task<GetByIdProductImageDto> GetByProductIdProductImageAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) throw new ArgumentNullException(nameof(id));

        try
        {
            var responseMessage = await _httpClient.GetAsync($"productimages/ProductImagesByProductId/{id}");
            if (!responseMessage.IsSuccessStatusCode)
                throw new ApplicationException($"Failed to get product images for product ID {id}.");

            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductImageDto>();
            return values ?? throw new ApplicationException("Product images not found for the provided product ID.");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product images by product ID.", ex);
        }
    }
}
