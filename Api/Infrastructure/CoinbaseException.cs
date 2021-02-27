using System;
using System.Net;

namespace Coinbase_Portfolio_Tracker.Api.Infrastructure
{
    public class CoinbaseException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        
        public CoinbaseException(){}

        public CoinbaseException(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        public CoinbaseException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public CoinbaseException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }
}