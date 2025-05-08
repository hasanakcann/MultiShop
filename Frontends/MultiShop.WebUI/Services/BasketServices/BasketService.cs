using MultiShop.DtoLayer.BasketDtos;

namespace MultiShop.WebUI.Services.BasketServices;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;

    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient cannot be null.");
    }

    public async Task AddBasketItem(BasketItemDto basketItemDto)
    {
        if (basketItemDto == null)
            throw new ArgumentNullException(nameof(basketItemDto), "Basket item cannot be null.");

        var basket = await GetBasket() ?? new BasketTotalDto
        {
            BasketItems = new List<BasketItemDto>()
        };

        var existingItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += basketItemDto.Quantity;
        }
        else
        {
            basket.BasketItems.Add(basketItemDto);
        }

        await SaveBasket(basket);
    }


    public async Task DeleteBasket()
    {
        var response = await _httpClient.DeleteAsync("baskets");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("We couldn't delete your basket at the moment. Please try again later or contact support.");
    }

    public async Task<BasketTotalDto?> GetBasket()
    {
        var response = await _httpClient.GetAsync("baskets");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Unable to retrieve basket. Please try again later.");

        var basket = await response.Content.ReadFromJsonAsync<BasketTotalDto>();

        return basket ?? new BasketTotalDto { BasketItems = new List<BasketItemDto>() };
    }

    public async Task<bool> RemoveBasketItem(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
            throw new ArgumentException("Product ID cannot be null or empty.", nameof(productId));

        var basket = await GetBasket() ?? new BasketTotalDto
        {
            BasketItems = new List<BasketItemDto>()
        };

        if (!basket.BasketItems.Any())
        {
            Console.WriteLine("Your basket is empty.");
            return false;
        }

        var deletedItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);

        if (deletedItem == null)
        {
            Console.WriteLine("Product not found in your basket.");
            return false;
        }

        basket.BasketItems.Remove(deletedItem);
        await SaveBasket(basket);

        Console.WriteLine("Product has been successfully removed from your basket.");
        return true;
    }


    public async Task SaveBasket(BasketTotalDto basketTotalDto)
    {
        if (basketTotalDto == null)
            throw new ArgumentNullException(nameof(basketTotalDto), "Basket data cannot be null.");

        var response = await _httpClient.PostAsJsonAsync("baskets", basketTotalDto);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Failed to save the basket. Please try again.");
    }
}
