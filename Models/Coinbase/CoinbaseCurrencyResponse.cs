using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase
{
    public class CoinbaseCurrencyResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("color")]
        public string Color { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("sort_index")]
        public int SortIndex { get; set; }
        
        [JsonProperty("exponent")]
        public int Exponent { get; set; }

        [JsonProperty("address_regex")]
        public string AddressRegex { get; set; }
        
        [JsonProperty("asset_id")]
        public string AssetId { get; set; }
        
        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}