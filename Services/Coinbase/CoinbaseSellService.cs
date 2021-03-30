using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;
using CoinbaseSell = Coinbase_Portfolio_Tracker.Models.Coinbase.Dto.CoinbaseSell;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseSellService
    {
        Task<CoinbaseSell> GetSellAsync(string accountId, string sellId);
    }

    public class CoinbaseSellService : RequestService, ICoinbaseSellService
    {
        public CoinbaseSellService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
        }

        public async Task<CoinbaseSell> GetSellAsync(string accountId, string sellId)
        {
            if (string.IsNullOrWhiteSpace(accountId) || string.IsNullOrWhiteSpace(sellId))
                throw new ArgumentNullException($"AccountId or SellId cannot be null, {accountId}, {sellId}");

            var sellResponse = await SendApiRequest<CoinbaseSellResponse>(HttpMethod.Get,
                $"accounts/{accountId}/sells/{sellId}");

            if (sellResponse?.Sell == null)
                throw new NullReferenceException("Sell transaction json response is null");

            var sell = sellResponse.Sell;

            return new CoinbaseSell()
            {
                SellAmount = sell.Amount.Amount,
                SellAmountCurrency = sell.Amount.Currency,
                FeeAmount = sell.Fee.Amount,
                FeeAmountCurrency = sell.Fee.Currency,
                SubtotalAmount = sell.Subtotal.Amount,
                SubtotalAmountCurrency = sell.Subtotal.Currency,
                TotalAmount = sell.Total.Amount,
                TotalAmountCurrency = sell.Total.Currency
            };
        }
    }
}