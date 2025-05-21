using Newtonsoft.Json;

namespace MultiShop.RapidApi.Models.ECommerce;

public class ECommerceData
{
    [JsonProperty("offers")]
    public List<ECommerceOffer> Offers { get; set; }

    [JsonProperty("product_rating")]
    public float ProductRating { get; set; }

    [JsonProperty("product_num_offers")]
    public int ProductNumOffers { get; set; }
}