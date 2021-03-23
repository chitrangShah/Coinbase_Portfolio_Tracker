using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Responses
{
    public class CoinbaseResourceResponseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("resource")]
        public string Resource { get; set; }
        
        [JsonProperty("resource_path")]
        public string ResourcePath { get; set; }
    }
}