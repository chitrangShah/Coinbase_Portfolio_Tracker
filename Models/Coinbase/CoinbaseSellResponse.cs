using System;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseSellResponse
    {
        [JsonProperty("data")]
        public CoinbaseSell Sell { get; set; }
    }

    public class CoinbaseSell
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("amount")]
        public CoinbasePriceResponseDetails Amount { get; set; }
        
        [JsonProperty("total")]
        public CoinbasePriceResponseDetails Total { get; set; }
        
        [JsonProperty("subtotal")]
        public CoinbasePriceResponseDetails Subtotal { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime Created_At { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTime Updated_At { get; set; }
        
        [JsonProperty("fee")]
        public CoinbasePriceResponseDetails Fee { get; set; }
    }
}