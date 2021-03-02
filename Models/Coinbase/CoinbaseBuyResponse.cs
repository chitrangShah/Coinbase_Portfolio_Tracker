using System;
using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseBuyResponse
    {
        [JsonPropertyName("data")]
        public CoinbaseBuyResponseDetails Buy { get; set; }
    }

    public class CoinbaseBuyResponseDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("amount")]
        public CoinbasePriceResponseDetails Amount { get; set; }
        
        [JsonPropertyName("total")]
        public CoinbasePriceResponseDetails Total { get; set; }
        
        [JsonPropertyName("subtotal")]
        public CoinbasePriceResponseDetails Subtotal { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime Created_At { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime Updated_At { get; set; }
        
        [JsonPropertyName("fee")]
        public CoinbasePriceResponseDetails Fee { get; set; }
    }
}