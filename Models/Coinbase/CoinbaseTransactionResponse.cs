using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseTransactionResponse
    {
        [JsonProperty("data")]
        public List<CoinbaseTransactionResponseDetails> Transactions { get; set; }
    }

    public class CoinbaseTransactionResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("amount")]
        public CoinbasePriceResponseDetails Amount { get; set; }
        
        [JsonProperty("native_amount")]
        public CoinbasePriceResponseDetails Native_Amount { get; set; }
        
        [JsonProperty("created_at")]
        public DateTimeOffset? Created_At { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTimeOffset? Updated_At { get; set; }
        
        [JsonProperty("buy")]
        public CoinbaseTransactionBuyResponseDetails Buy { get; set; }
        
        [JsonProperty("sell")]
        public CoinbaseTransactionSendResponseDetails Sell { get; set; }
    }

    public class CoinbaseTransactionBuyResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string Resource_Path { get; set; }
    }
    
    public class CoinbaseTransactionSendResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string Resource_Path { get; set; }
    }
}