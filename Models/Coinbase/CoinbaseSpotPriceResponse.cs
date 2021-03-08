using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseSpotPriceResponse
    {
        [JsonProperty("data")]
        public CoinbasePriceResponseDetails SpotPrice { get; set; }
    }
}