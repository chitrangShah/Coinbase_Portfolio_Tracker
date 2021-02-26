using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase
{
    public abstract class CoinbasePriceResponseDetails
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}