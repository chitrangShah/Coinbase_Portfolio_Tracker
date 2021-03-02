using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseSpotPriceResponse
    {
        [JsonPropertyName("data")]
        public CoinbasePriceResponseDetails SpotPrice { get; set; }
    }
}