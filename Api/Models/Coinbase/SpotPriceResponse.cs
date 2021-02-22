using System.Text.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase
{
    public class SpotPriceResponse
    {
        [JsonPropertyName("data")]
        public PriceResponse SpotPrice { get; set; }
    }
}