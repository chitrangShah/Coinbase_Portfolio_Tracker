using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<IEnumerable<CoinbaseAccountResponse>> GetAllAccountsAsync();
    }

    public class CoinbaseAccountService : RequestService, ICoinbaseAccountService
    {
        public CoinbaseAccountService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
            
        }
        
        public async Task<IEnumerable<CoinbaseAccountResponse>> GetAllAccountsAsync()
        {
            return await SendApiRequest<List<CoinbaseAccountResponse>>(HttpMethod.Get, "/accounts");
        }
    }
}