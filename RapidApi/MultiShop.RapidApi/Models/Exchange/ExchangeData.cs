using Newtonsoft.Json;

namespace MultiShop.RapidApi.Models.Exchange;

public class ExchangeData
{
    [JsonProperty("from_symbol")]
    public string FromSymbol { get; set; }

    [JsonProperty("to_symbol")]
    public string ToSymbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("exchange_rate")]
    public float ExchangeRate { get; set; }

    [JsonProperty("previous_close")]
    public float PreviousClose { get; set; }

    [JsonProperty("last_update_utc")]
    public string LastUpdateUtc { get; set; }
}
