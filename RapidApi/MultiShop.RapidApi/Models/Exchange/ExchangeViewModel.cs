using Newtonsoft.Json;

namespace MultiShop.RapidApi.Models.Exchange;

public class ExchangeViewModel
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    [JsonProperty("data")]
    public ExchangeData Data { get; set; }
}
