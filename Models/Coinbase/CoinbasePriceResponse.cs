using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbasePriceResponseDetails
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }
        
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}