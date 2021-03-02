using System.Net.Http;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public interface ICoinbaseClient
    {
        HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod,
            string requestUri,
            string contentBody = "");
    }
}