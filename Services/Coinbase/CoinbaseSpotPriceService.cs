using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseSpotPriceService
    {
        Task<CoinbaseSpotPriceResponse> GetSpotPrice(string currencyPair);
    }

    public class CoinbaseSpotPriceService : RequestService, ICoinbaseSpotPriceService
    {
        public CoinbaseSpotPriceService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
        }

        public async Task<CoinbaseSpotPriceResponse> GetSpotPrice(string currencyPair)
        {
            return await SendApiRequest<CoinbaseSpotPriceResponse>(
                HttpMethod.Get, 
                $"/prices/{currencyPair}/spot");
        }
    }
    
}