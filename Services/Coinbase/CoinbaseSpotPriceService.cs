using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseSpotPriceService
    {
        Task<CoinbasePrice> GetSpotPriceAsync(string currencyPair);
    }

    public class CoinbaseSpotPriceService : RequestService, ICoinbaseSpotPriceService
    {
        public CoinbaseSpotPriceService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
        }

        public async Task<CoinbasePrice> GetSpotPriceAsync(string currencyPair)
        {
            var spotPriceResponse = await SendApiRequest<CoinbaseSpotPriceResponse>(
                HttpMethod.Get, 
                $"prices/{currencyPair}/spot",
                "",
                false);

            return new CoinbasePrice()
            {
                Amount = spotPriceResponse.SpotPrice.Amount,
                Currency = spotPriceResponse.SpotPrice.Currency
            };
        }
    }
    
}