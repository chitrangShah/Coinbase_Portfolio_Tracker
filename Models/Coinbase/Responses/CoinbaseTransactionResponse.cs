using System;
using System.Collections.Generic;
using Coinbase_Portfolio_Tracker.Infrastructure.JsonConverters;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Responses
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
        public CoinbasePriceResponseDetails NativeAmount { get; set; }
        
        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
    
    [JsonConverter(typeof(CoinbaseTransactionServiceConverter))]
    public class CoinbaseTransactionBuyResponseDetails : CoinbaseTransactionResponseDetails
    {
        [JsonProperty("buy")]
        public CoinbaseResourceResponseDetails Buy { get; set; }
    }
    
    [JsonConverter(typeof(CoinbaseTransactionServiceConverter))]
    public class CoinbaseTransactionSellResponseDetails : CoinbaseTransactionResponseDetails
    {
        [JsonProperty("sell")]
        public CoinbaseResourceResponseDetails Sell { get; set; }
    }
}