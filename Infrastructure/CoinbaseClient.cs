using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Coinbase_Portfolio_Tracker.Shared;
using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public class CoinbaseClient : ICoinbaseClient
    {
        private readonly ICoinbaseAuthenticator _coinbaseAuthenticator;
        private readonly IConfiguration _configuration;

        public CoinbaseClient(ICoinbaseAuthenticator coinbaseAuthenticator, 
            IConfiguration configuration)
        {
            _coinbaseAuthenticator = coinbaseAuthenticator;
            _configuration = configuration;
        }
        
        public HttpRequestMessage CreateHttpRequestMessage(HttpMethod httpMethod, string requestUri, string contentBody = "")
        {
            var apiUri = "";
            var requestMessage = new HttpRequestMessage(httpMethod, new Uri(new Uri(apiUri), requestUri))
            {
                Content = contentBody == string.Empty
                    ? null
                    : new StringContent(contentBody, Encoding.UTF8, "application/json")
            };

            var timestamp = Utils.CurrentUnixTimestamp();

            if (_coinbaseAuthenticator == null)
            {
                throw new ArgumentNullException($"{_coinbaseAuthenticator} has not been initialized.");
            }

            var messageSignature = _coinbaseAuthenticator.CreateSignature(timestamp, httpMethod, requestUri,
                _coinbaseAuthenticator.ApiSecret, contentBody);
            
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessKey, _coinbaseAuthenticator.ApiKey);
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessTimestamp, timestamp.ToString("F0", CultureInfo.InvariantCulture));
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessSign, messageSignature);

            return requestMessage;
        }
    }
}