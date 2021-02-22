using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase
{
    public class AccountResponse
    {
        [JsonPropertyName("data")]
        public List<Account> Accounts { get; set; }
    }

    public class Account
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("primary")]
        public bool IsPrimaryAccount { get; set; }
        
        [JsonPropertyName("type")]
        public List<string> Type { get; set; }
        
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("balance")] 
        public PriceResponse Balance { get; set; }
        
        [JsonPropertyName("created_at")]
        public DateTime Created_At { get; set; }
        
        [JsonPropertyName("updated_at")]
        public DateTime Updated_At { get; set; }
    }
}