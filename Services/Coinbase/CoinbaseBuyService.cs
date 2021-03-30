using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseBuyService
    {
        Task<CoinbaseBuy> GetBuyAsync(string accountId, string buyId);
    }

    public class CoinbaseBuyService : RequestService, ICoinbaseBuyService
    {
        public CoinbaseBuyService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
            
        }
        public async Task<CoinbaseBuy> GetBuyAsync(string accountId, string buyId)
        {
            if (string.IsNullOrWhiteSpace(accountId) || string.IsNullOrWhiteSpace(buyId))
                throw new ArgumentNullException($"AccountId or BuyId cannot be null, {accountId}, {buyId}");
            
            var buyResponse = await SendApiRequest<CoinbaseBuyResponse>(HttpMethod.Get,
                $"accounts/{accountId}/buys/{buyId}");

            if (buyResponse?.Buy == null)
                throw new NullReferenceException("Buy transaction json response is null");

            var buy = buyResponse.Buy;

            return new CoinbaseBuy()
            {
                BuyAmount = buy.Amount.Amount,
                BuyAmountCurrency = buy.Amount.Currency,
                FeeAmount = buy.Fee.Amount,
                FeeAmountCurrency = buy.Fee.Currency,
                SubtotalAmount = buy.Subtotal.Amount,
                SubtotalAmountCurrency = buy.Subtotal.Currency,
                TotalAmount = buy.Total.Amount,
                TotalAmountCurrency = buy.Total.Currency
            };
        }
    }
}