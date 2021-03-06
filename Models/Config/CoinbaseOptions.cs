namespace Coinbase_Portfolio_Tracker.Models.Config
{
    public class CoinbaseOptions
    {
        public const string SectionName = "Coinbase";
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Endpoint { get; set; }
        public string ApiVersionDate { get; set; }
    }
}