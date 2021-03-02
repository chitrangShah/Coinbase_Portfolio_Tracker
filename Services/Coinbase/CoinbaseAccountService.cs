using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Infrastructure;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
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