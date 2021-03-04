using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using Coinbase_Portfolio_Tracker.Infrastructure;
using Coinbase_Portfolio_Tracker.Models.Config;
using Coinbase_Portfolio_Tracker.Shared;
using Microsoft.Extensions.Options;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseConnectService
    {
        HttpRequestMessage CreateApiRequestMessage(
            HttpMethod httpMethod, 
            string requestUri, 
            string contentBody = "");
    }
    
    public class CoinbaseConnectService : ICoinbaseConnectService
    {
        private readonly ICoinbaseAuthenticator _coinbaseAuthenticator;
        private readonly CoinbaseOptions _coinbaseOptions;

        public CoinbaseConnectService(ICoinbaseAuthenticator coinbaseAuthenticator,
            IOptionsMonitor<CoinbaseOptions> coinbaseOptions)
        {
            _coinbaseAuthenticator = coinbaseAuthenticator;
            _coinbaseOptions = coinbaseOptions.CurrentValue;
        }
        
        public HttpRequestMessage CreateApiRequestMessage(HttpMethod httpMethod, string requestUri, string contentBody = "")
        {
            var apiUri = _coinbaseOptions.Endpoint;
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
                _coinbaseOptions.ClientSecret, contentBody);
            
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessKey, _coinbaseOptions.ClientId);
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessTimestamp, timestamp.ToString("F0", CultureInfo.InvariantCulture));
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessSign, messageSignature);

            return requestMessage;
        }
    }
}