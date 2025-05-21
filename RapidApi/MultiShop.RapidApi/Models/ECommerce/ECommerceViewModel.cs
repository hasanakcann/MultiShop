using Newtonsoft.Json;

namespace MultiShop.RapidApi.Models.ECommerce;

public class ECommerceViewModel
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    [JsonProperty("data")]
    public ECommerceData Data { get; set; }
}