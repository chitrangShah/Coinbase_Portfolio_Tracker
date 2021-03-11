using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;

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
                    IsABuy = transaction.Buy != null,
                    IsASell = transaction.Sell != null,
                    Type = transaction.Type,
                    TransactionAmount = transaction.Amount.Amount,
                    TransactionAmountCurrency = transaction.Amount.Currency,
                    TransactionId = transaction.Id,
                    TransactionNativeAmount = transaction.Native_Amount.Amount,
                    TransactionNativeAmountCurrency = transaction.Native_Amount.Currency,
                    TransactionCreatedDate = transaction.Created_At
                }).ToList();
        }
    }
}