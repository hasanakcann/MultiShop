using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;

namespace MultiShop.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService), "Redis service cannot be null.");
    }

    public async Task DeleteBasket(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

        var redisDatabase = _redisService.GetDatabase();
        if (redisDatabase == null)
            throw new InvalidOperationException("Redis database connection is null.");

        await redisDatabase.KeyDeleteAsync(userId);
    }

    public async Task<BasketTotalDto?> GetBasket(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("UserId cannot be null or empty.", nameof(userId));

        var redisDatabase = _redisService.GetDatabase();
        if (redisDatabase == null)
            throw new InvalidOperationException("Redis database connection is null.");

        var serializedBasketData = await redisDatabase.StringGetAsync(userId);

        if (serializedBasketData.IsNullOrEmpty)
            return null;

        try
        {
            var basket = JsonSerializer.Deserialize<BasketTotalDto>(serializedBasketData);
            if (basket == null)
                throw new InvalidOperationException("Failed to deserialize basket data into BasketTotalDto.");

            return basket;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to deserialize basket data.", ex);
        }
    }

    public async Task<bool> SaveBasket(BasketTotalDto basketTotalDto)
    {
        if (basketTotalDto == null)
            throw new ArgumentNullException(nameof(basketTotalDto), "Basket data cannot be null.");

        if (string.IsNullOrWhiteSpace(basketTotalDto.UserId))
            throw new ArgumentException("UserId cannot be null or empty.", nameof(basketTotalDto.UserId));

        var redisDatabase = _redisService.GetDatabase();
        if (redisDatabase == null)
            throw new InvalidOperationException("Redis database connection is null.");

        var serializedBasket = JsonSerializer.Serialize(basketTotalDto);
        if (string.IsNullOrEmpty(serializedBasket))
            throw new InvalidOperationException("Failed to serialize basket data.");

        return await redisDatabase.StringSetAsync(basketTotalDto.UserId, serializedBasket);
    }
}
