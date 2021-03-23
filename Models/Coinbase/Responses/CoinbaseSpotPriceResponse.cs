using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Responses
{
    public class CoinbaseSpotPriceResponse
    {
        [JsonProperty("data")]
        public CoinbasePriceResponseDetails SpotPrice { get; set; }
    }
}