using System;
using System.Collections.Generic;
using Coinbase_Portfolio_Tracker.Infrastructure.JsonConverters;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseTransactionResponse
    {
        [JsonProperty("data")]
        public List<CoinbaseTransactionResponseDetails> Transactions { get; set; }
    }

    [JsonConverter(typeof(CoinbaseTransactionServiceConverter))]
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
    }

    [JsonConverter(typeof(CoinbaseTransactionServiceConverter))]
    public class CoinbaseTransactionBuyResponseDetails : CoinbaseTransactionResponseDetails
    {
        [JsonProperty("id")]
        public string BuyId { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string Resource_Path { get; set; }
    }
    
    [JsonConverter(typeof(CoinbaseTransactionServiceConverter))]
    public class CoinbaseTransactionSellResponseDetails : CoinbaseTransactionResponseDetails
    {
        [JsonProperty("id")]
        public string SellId { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string Resource_Path { get; set; }
    }
}