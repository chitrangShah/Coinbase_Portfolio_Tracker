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
            string contentBody = "",
            bool isAuthenticated = true);
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
        
        public HttpRequestMessage CreateApiRequestMessage(HttpMethod httpMethod, 
            string requestUri, string contentBody = "", bool isAuthenticated = true)
        {
            var apiUri = _coinbaseOptions.Endpoint;
            var fullRequestUri = new Uri(new Uri(apiUri), requestUri);
            var requestMessage = new HttpRequestMessage(httpMethod, fullRequestUri)
            {
                Content = contentBody == string.Empty
                    ? null
                    : new StringContent(contentBody, Encoding.UTF8, "application/json")
            };

            if (!isAuthenticated)
            {
                return requestMessage;
            }

            DateTimeOffset epoch = DateTimeOffset.UnixEpoch;
            DateTimeOffset now = DateTimeOffset.UtcNow;
 
            TimeSpan ts = now.Subtract(epoch);
            string timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(CultureInfo.CurrentCulture);
                //ts.TotalSeconds.ToString("F0");

            if (_coinbaseAuthenticator == null)
            {
                throw new ArgumentNullException($"{_coinbaseAuthenticator} has not been initialized.");
            }

            var messageSignature = _coinbaseAuthenticator.CreateSignature(timestamp, httpMethod, fullRequestUri.AbsolutePath,
                _coinbaseOptions.ClientSecret, contentBody);
            
            requestMessage.Headers.Add("User-Agent", "CoinbaseApi");
            requestMessage.Headers.Add(Constants.CoinbaseHeaderApiVersionDate, _coinbaseOptions.ApiVersionDate);
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessKey, _coinbaseOptions.ClientId);
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessSign, messageSignature);
            requestMessage.Headers.Add(Constants.CoinbaseHeaderAccessTimestamp, timestamp);

            return requestMessage;
        }
    }
}