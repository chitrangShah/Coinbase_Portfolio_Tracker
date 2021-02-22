using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase
{
    public abstract class PriceResponse
    {
        [JsonPropertyName("amount")]
        public float Amount { get; set; }
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}