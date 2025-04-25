namespace MultiShop.Basket.Dtos;

public class BasketTotalDto
{
    public string UserId { get; set; }
    public string DiscountCode { get; set; }
    public int DiscountRate { get; set; }
    public List<BasketItemDto> BasketItems { get; set; } = new();
    public decimal TotalPrice => BasketItems?.Sum(item => item.Price * item.Quantity) ?? 0;
}
