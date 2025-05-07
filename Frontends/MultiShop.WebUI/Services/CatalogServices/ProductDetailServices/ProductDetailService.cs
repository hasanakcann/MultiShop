using MultiShop.DtoLayer.CatalogDtos.ProductDetailDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.Services.CatalogServices.ProductDetailServices;

public class ProductDetailService : IProductDetailService
{
    private readonly HttpClient _httpClient;

    public ProductDetailService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
    {
        if (createProductDetailDto == null)
        {
            throw new ArgumentNullException(nameof(createProductDetailDto), "Product detail cannot be null.");
        }

        try
        {
            var response = await _httpClient.PostAsJsonAsync("productdetails", createProductDetailDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while creating the product detail.", ex);
        }
    }

    public async Task DeleteProductDetailAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Product ID cannot be null or empty.");
        }

        try
        {
            var response = await _httpClient.DeleteAsync($"productdetails?id={id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the product detail.", ex);
        }
    }

    public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
    {
        try
        {
            var responseMessage = await _httpClient.GetAsync("productdetails");
            responseMessage.EnsureSuccessStatusCode();
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDetailDto>>(jsonData);

            return values ?? new List<ResultProductDetailDto>();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all product details.", ex);
        }
    }

    public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
    {
        if (updateProductDetailDto == null)
        {
            throw new ArgumentNullException(nameof(updateProductDetailDto), "Product detail cannot be null.");
        }

        try
        {
            var response = await _httpClient.PutAsJsonAsync("productdetails", updateProductDetailDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the product detail.", ex);
        }
    }

    public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Product ID cannot be null or empty.");
        }

        try
        {
            var responseMessage = await _httpClient.GetAsync($"productdetails/{id}");
            responseMessage.EnsureSuccessStatusCode();
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDetailDto>();

            return values ?? new GetByIdProductDetailDto();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product detail by ID.", ex);
        }
    }

    public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            throw new ArgumentNullException(nameof(id), "Product ID cannot be null or empty.");
        }

        try
        {
            var responseMessage = await _httpClient.GetAsync($"productdetails/getproductdetailbyproductid/{id}");
            responseMessage.EnsureSuccessStatusCode();
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDetailDto>();

            return values ?? new GetByIdProductDetailDto();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the product detail by product ID.", ex);
        }
    }
}
