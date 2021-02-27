using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Api.Models.Coinbase.Dto;
using System.Net.Http;
using Coinbase_Portfolio_Tracker.Api.Infrastructure;

namespace Coinbase_Portfolio_Tracker.Api.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<IEnumerable<CoinbaseAccount>> GetAccountsAsync(string apiKey);
    }

    public class CoinbaseAccountService : ICoinbaseAccountService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICoinbaseAuthenticator _coinbaseAuthenticator;

        public CoinbaseAccountService(IHttpClientFactory httpClientFactory, 
            ICoinbaseAuthenticator coinbaseAuthenticator)
        {
            _httpClientFactory = httpClientFactory;
            _coinbaseAuthenticator = coinbaseAuthenticator;
        }
        
        public Task<IEnumerable<CoinbaseAccount>> GetAccountsAsync(string apiKey)
        {
            throw new System.NotImplementedException();
        }
    }
}