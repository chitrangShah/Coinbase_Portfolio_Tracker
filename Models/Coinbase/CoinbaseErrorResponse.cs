using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseErrorResponse
    {
        [JsonPropertyName("errors")]
        public List<CoinbaseErrorResponseDetails> Errors { get; set; }
    }

    public class CoinbaseErrorResponseDetails
    {
        [JsonPropertyName("id")]
        public string ErrorId { get; set; }
        
        [JsonPropertyName("message")]
        public string ErrorMessage { get; set; }
        
        [JsonPropertyName("url")]
        public string ErrorDocumentationUrl { get; set; }
    }
}