using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseAccountService
    {
        Task<CoinbaseAccount> GetAccountInfo();
    }

    public class CoinbaseAccountService : RequestService, ICoinbaseAccountService
    {
        private readonly ICoinbaseSpotPriceService _coinbaseSpotPriceService;
        
        public CoinbaseAccountService(ICoinbaseConnectService coinbaseConnectService,
            ICoinbaseSpotPriceService coinbaseSpotPriceService) 
            : base(coinbaseConnectService)
        {
            _coinbaseSpotPriceService = coinbaseSpotPriceService;
        }
        
        public async Task<CoinbaseAccount> GetAccountInfo()
        {
            var cbAccountResponse = await SendApiRequest<CoinbaseAccountResponse>(HttpMethod.Get, "accounts");
            
            var currency_name = string.Empty;

            return new CoinbaseAccount();
        }
    }
}