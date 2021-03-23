using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Responses
{
    public class CoinbaseErrorResponse
    {
        [JsonProperty("errors")]
        public List<CoinbaseErrorResponseDetails> Errors { get; set; }
    }

    public class CoinbaseErrorResponseDetails
    {
        [JsonProperty("id")]
        public string ErrorId { get; set; }
        
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
        
        [JsonProperty("url")]
        public string ErrorDocumentationUrl { get; set; }
    }
}