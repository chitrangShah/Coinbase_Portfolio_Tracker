using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Infrastructure;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Coinbase_Portfolio_Tracker.Services
{
    public abstract class RequestService
    {
        private static readonly HttpClient HttpClient = new();
        private readonly ICoinbaseConnectService _coinbaseConnectService;

        protected RequestService(ICoinbaseConnectService coinbaseConnectService)
        {
            _coinbaseConnectService = coinbaseConnectService;
        }

        protected async Task<T> SendApiRequest<T>(HttpMethod httpMethod, 
            string requestUri, string content = "", bool isAuthenticated = true)
        {
            var httpResponseMessage = await SendHttpRequestAsync(httpMethod, requestUri, content, isAuthenticated);
            var contentBody = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var serializerSettings = new JsonSerializerSettings()
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    // property names might include '_' eg: last_name
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
                
            return JsonConvert.DeserializeObject<T>(contentBody, serializerSettings);
        }

        private async Task<HttpResponseMessage> SendHttpRequestAsync(
            HttpMethod httpMethod,
            string requestUri,
            string content = "",
            bool isAuthenticated = true)
        {
            var httpRequestMessage = _coinbaseConnectService.CreateApiRequestMessage(
                httpMethod, requestUri, content, isAuthenticated);
            
            var httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return httpResponseMessage;
            }

            var contentBody = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            CoinbaseErrorResponse errorResponse;
            
            try
            {
                errorResponse = JsonConvert.DeserializeObject<CoinbaseErrorResponse>(contentBody);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (errorResponse?.Errors == null) 
                throw new HttpRequestException();

            var ex = new CoinbaseException(errorResponse.Errors[0].ErrorMessage)
            {
                RequestMessage = httpRequestMessage,
                ResponseMessage = httpResponseMessage,
                StatusCode = httpResponseMessage.StatusCode
            };

            throw ex;

        }
    }
}