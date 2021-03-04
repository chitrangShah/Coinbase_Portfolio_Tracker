using System.Net.Http;
using System.Threading.Tasks;
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

        public async Task<T> SendApiRequest<T>(HttpMethod httpMethod, string requestUri, string content = "")
        {
            var httpResponseMessage = await SendHttpRequestAsync(httpMethod, requestUri, content);
            var contentBody = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var serializerSettings = new JsonSerializerSettings()
            {
                FloatParseHandling = FloatParseHandling.Decimal,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
                
            return JsonConvert.DeserializeObject<T>(contentBody, serializerSettings);
        }

        private async Task<HttpResponseMessage> SendHttpRequestAsync(
            HttpMethod httpMethod, 
            string requestUri,
            string content = "")
        {
            var httpRequestMessage = string.IsNullOrWhiteSpace(content)
                ? _coinbaseConnectService.CreateApiRequestMessage(httpMethod, requestUri)
                : _coinbaseConnectService.CreateApiRequestMessage(httpMethod, requestUri, content);
            
            var httpResponseMessage = await HttpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return httpResponseMessage;
            }

            var contentBody = await httpResponseMessage.Content.ReadAsStringAsync();
            
            // TODO: Fix error handling for Coinbase error type
            throw new HttpRequestException();
        }
    }
}