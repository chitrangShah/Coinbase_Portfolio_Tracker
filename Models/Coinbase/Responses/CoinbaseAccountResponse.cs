using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Responses
{
    public class CoinbaseAccountResponse
    {
        [JsonProperty("data")]
        public List<CoinbaseAccountResponseDetails> Accounts { get; set; }
    }

    public class CoinbaseAccountResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("primary")]
        public bool IsPrimaryAccount { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("currency")]
        public CoinbaseCurrencyResponse Currency { get; set; }

        [JsonProperty("balance")] 
        public CoinbasePriceResponseDetails Balance { get; set; }
        
        [JsonProperty("created_at")]
        public DateTimeOffset? Created_At { get; set; }
        
        [JsonProperty("updated_at")]
        public DateTimeOffset? Updated_At { get; set; }
    }
}