using System;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseBuyResponse
    {
        [JsonProperty("data")]
        public CoinbaseBuyResponseDetails Buy { get; set; }
    }

    public class CoinbaseBuyResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("payment_method")]
        public CoinbaseResourceResponseDetails PaymentMethod { get; set; }
        
        [JsonProperty("transaction")]
        public CoinbaseResourceResponseDetails Transaction { get; set; }
        
        [JsonProperty("amount")]
        public CoinbasePriceResponseDetails Amount { get; set; }
        
        [JsonProperty("total")]
        public CoinbasePriceResponseDetails Total { get; set; }
        
        [JsonProperty("subtotal")]
        public CoinbasePriceResponseDetails Subtotal { get; set; }
        
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string ResourcePath { get; set; }
        
        [JsonProperty("committed")]
        public bool Committed { get; set; }
        
        [JsonProperty("instant")]
        public bool Instant { get; set; }
        
        [JsonProperty("fee")]
        public CoinbasePriceResponseDetails Fee { get; set; }
        
        [JsonProperty("payout_at")]
        public DateTimeOffset? PayoutAt { get; set; }
    }
}