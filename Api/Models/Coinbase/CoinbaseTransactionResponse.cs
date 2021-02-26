using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase
{
    public class CoinbaseTransactionResponse
    {
        [JsonPropertyName("data")]
        public List<CoinbaseTransactionResponseDetails> Transactions { get; set; }
    }

    public class CoinbaseTransactionResponseDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        [JsonPropertyName("amount")]
        public CoinbasePriceResponseDetails Amount { get; set; }
        
        [JsonPropertyName("native_amount")]
        public CoinbasePriceResponseDetails Native_Amount { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime Created_At { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime Updated_At { get; set; }
        
        [JsonPropertyName("buy")]
        public CoinbaseTransactionBuyResponseDetails Buy { get; set; }
        
        [JsonPropertyName("sell")]
        public CoinbaseTransactionSendResponseDetails Sell { get; set; }
    }

    public class CoinbaseTransactionBuyResponseDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("resource")]
        public string Resource { get; set; }
        
        [JsonPropertyName("resource_path")]
        public string Resource_Path { get; set; }
    }
    
    public class CoinbaseTransactionSendResponseDetails
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("resource")]
        public string Resource { get; set; }
        
        [JsonPropertyName("resource_path")]
        public string Resource_Path { get; set; }
    }
}