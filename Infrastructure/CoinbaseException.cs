using System;
using System.Net;
using System.Net.Http;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public class CoinbaseException : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpRequestMessage RequestMessage { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }
        
        public CoinbaseException(string message) 
            : base(message)
        {
        }
    }
}