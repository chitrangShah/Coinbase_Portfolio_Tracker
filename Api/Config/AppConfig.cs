namespace Coinbase_Portfolio_Tracker.Api.Config
{
    public class AppConfig
    {
        public Coinbase Coinbase { get; set; }
        public Google GoogleSpreadsheet { get; set; }
    }

    public class Coinbase
    {
        public string ApiKey { get; set; }
    }

    public class Google
    {
        public string Type { get; set; }
        public string ProjectId { get; set; }
        public string PrivateKeyId { get; set; }
        public string PrivateKey { get; set; }
        public string ClientEmail { get; set; }
        public string ClientId { get; set; }
    }
}