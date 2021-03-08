using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<List<CoinbaseAccount>> GetAllAccountsAsync();
    }

    public class CoinbaseAccountService : RequestService, ICoinbaseAccountService
    {
        public CoinbaseAccountService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
            
        }
        
        public async Task<List<CoinbaseAccount>> GetAllAccountsAsync()
        {
            var cbAccountsResponse = await SendApiRequest<CoinbaseAccountResponse>(HttpMethod.Get, "accounts");

            return cbAccountsResponse.Accounts
                .Select(account => new CoinbaseAccount() 
                    {
                        Id = account.Id,
                        Name = account.Name,
                        AccountCurrency = account.Currency.Code, 
                        BalanceAmount = account.Balance.Amount, 
                        BalanceAmountCurrency = account.Balance.Currency
                    }).ToList();
        }
    }
}