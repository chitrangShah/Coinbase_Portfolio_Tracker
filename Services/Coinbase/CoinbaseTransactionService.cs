using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Extensions;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;

namespace Coinbase_Portfolio_Tracker.Services.Coinbase
{
    public interface ICoinbaseTransactionService
    {
        Task<List<CoinbaseTransaction>> GetAllTransactionsAsync(string accountId);
    }

    public class CoinbaseTransactionService : RequestService, ICoinbaseTransactionService
    {
        public CoinbaseTransactionService(ICoinbaseConnectService coinbaseConnectService) 
            : base(coinbaseConnectService)
        {
        }

        public async Task<List<CoinbaseTransaction>> GetAllTransactionsAsync(string accountId)
        {
            if (string.IsNullOrWhiteSpace(accountId))
            {
                throw new ArgumentNullException($"Please provide a valid {accountId} for transactions.");
            }
            
            var transactionsResponse = await SendApiRequest<CoinbaseTransactionResponse>(HttpMethod.Get,
                $"accounts/{accountId}/transactions");

            return transactionsResponse
                .Transactions
                .Select(transaction => new CoinbaseTransaction()
                {
                    TransactionId = transaction.Id,
                    Type = transaction.Type,
                    TransactionAmount = transaction.Amount.Amount,
                    TransactionAmountCurrency = transaction.Amount.Currency,
                    TransactionNativeAmount = transaction.NativeAmount.Amount,
                    TransactionNativeAmountCurrency = transaction.NativeAmount.Currency,
                    TransactionCreatedDate = transaction.CreatedAt,
                    
                    // This can improved to use Strategy pattern for other types 
                    // For now, we only care about buy and sell
                    Buy = transaction.Buy.ToBuyTransaction(),
                    Sell = transaction.Sell.ToSellTransaction()
                }).ToList();
        }
    }
}