using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<IEnumerable<CoinbaseAccount>> GetAllAccountsAsync();
    }

    public class CoinbaseAccountService : RequestService, ICoinbaseAccountService
    {
        public CoinbaseAccountService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
            
        }
        
        public async Task<IEnumerable<CoinbaseAccount>> GetAllAccountsAsync()
        {
            return await SendApiRequest<List<CoinbaseAccount>>(HttpMethod.Get, "/accounts");
        }
    }
}