using Newtonsoft.Json;

namespace MultiShop.RapidApi.Models.ECommerce;

public class ECommerceOffer
{
    [JsonProperty("offer_id")]
    public string OfferId { get; set; }

    [JsonProperty("offer_title")]
    public string OfferTitle { get; set; }

    [JsonProperty("offer_page_url")]
    public string OfferPageUrl { get; set; }

    [JsonProperty("price")]
    public string Price { get; set; }

    [JsonProperty("shipping")]
    public string Shipping { get; set; }

    [JsonProperty("offer_badge")]
    public string OfferBadge { get; set; }

    [JsonProperty("on_sale")]
    public bool OnSale { get; set; }

    [JsonProperty("original_price")]
    public string OriginalPrice { get; set; }

    [JsonProperty("product_condition")]
    public string ProductCondition { get; set; }

    [JsonProperty("store_name")]
    public string StoreName { get; set; }

    [JsonProperty("store_rating")]
    public string StoreRating { get; set; }

    [JsonProperty("store_review_count")]
    public int StoreReviewCount { get; set; }

    [JsonProperty("store_reviews_page_url")]
    public string StoreReviewsPageUrl { get; set; }

    [JsonProperty("store_favicon")]
    public string StoreFavicon { get; set; }

    [JsonProperty("payment_methods")]
    public string PaymentMethods { get; set; }

    [JsonProperty("percent_off")]
    public string PercentOff { get; set; }

    [JsonProperty("coupon_discount_percent")]
    public string CouponDiscountPercent { get; set; }
}