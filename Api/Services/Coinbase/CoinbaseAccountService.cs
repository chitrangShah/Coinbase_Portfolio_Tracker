using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Api.Models.Coinbase.Dto;
using System.Net.Http;
using Coinbase_Portfolio_Tracker.Api.Config;
using Microsoft.Extensions.Options;

namespace Coinbase_Portfolio_Tracker.Api.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<List<CoinbaseAccount>> GetAccounts(string apiKey);
    }

    public class CoinbaseAccountService : ICoinbaseAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppConfig _appConfig;

        public CoinbaseAccountService(IOptions<AppConfig> opts, IHttpClientFactory httpClientFactory)
        {
            _appConfig = opts.Value;
            _httpClientFactory = httpClientFactory;
        }
        
        public Task<List<CoinbaseAccount>> GetAccounts(string apiKey)
        {
            throw new System.NotImplementedException();
        }
    }
}