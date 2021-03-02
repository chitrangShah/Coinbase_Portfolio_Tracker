using System.Net.Http;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public interface ICoinbaseAuthenticator
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public string CreateSignature(double timestamp,
            HttpMethod httpMethod,
            string requestPath,
            string apiSecret,
            string body = "");
    }
}