using System.Threading;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Coinbase_Portfolio_Tracker.Shared;
using Microsoft.Extensions.Hosting;

namespace Coinbase_Portfolio_Tracker
{
    public class App : IHostedService
    {
        private readonly ICoinbaseAccountService _coinbaseAccountService;
        private readonly ICoinbaseSpotPriceService _coinbaseSpotPriceService;
        private readonly ICoinbaseTransactionService _coinbaseTransactionService;
        private readonly ICoinbaseBuyService _coinbaseBuyService;

        public App(ICoinbaseAccountService coinbaseAccountService,
            ICoinbaseSpotPriceService coinbaseSpotPriceService,
            ICoinbaseTransactionService coinbaseTransactionService,
            ICoinbaseBuyService coinbaseBuyService)
        {
            _coinbaseAccountService = coinbaseAccountService;
            _coinbaseSpotPriceService = coinbaseSpotPriceService;
            _coinbaseTransactionService = coinbaseTransactionService;
            _coinbaseBuyService = coinbaseBuyService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var myCoinbasePerformance = new CoinbasePerformance();

            // ** Coinbase Api Steps **
            // Create service client to connect to coinbase
            // Get account info
            var accounts = await _coinbaseAccountService.GetAccountsAsync();

            foreach (var account in accounts)
            {
                if (account.AccountCurrency == Constants.USCurrencyCode || account.BalanceAmount == 0)
                {
                    // Do nothing
                }
                // Get current amount and totals
                else
                {
                    var currentSpotPrice = await _coinbaseSpotPriceService
                        .GetSpotPriceAsync(account.AccountCurrency + $"-{Constants.USCurrencyCode}");

                    var currencyDict = new CoinbaseCurrency
                    {
                        Symbol = account.AccountCurrency,
                        Quantity = account.BalanceAmount,
                        CurrentPrice = currentSpotPrice,
                        CurrentTotal = account.BalanceAmount
                    };

                    myCoinbasePerformance.Currencies.Add(currencyDict);

                        // Get transactions
                    var transactions = await _coinbaseTransactionService.GetAllTransactionsAsync(account.Id);

                    foreach (var transaction in transactions)
                    {
                        switch (transaction.Type)
                        {
                            case "buy":
                            {
                                var buyAmount = transaction.TransactionAmount;
                                
                                // buy price and fee
                                var buy = await _coinbaseBuyService.GetBuy(account.Id, transaction.Buy.Id);
                                var buyCost = buy.TotalAmount;
                                var buySubtotal = buy.SubtotalAmount;
                                var totalFee = buy.FeeAmount;

                                var buyOrder = new BuyOrder()
                                {
                                    Type = "buy",
                                    Datetime = transaction.TransactionCreatedDate,
                                    Symbol = transaction.TransactionAmountCurrency,
                                    Amount = buyAmount,
                                    Cost = buyCost,
                                    Invested = buySubtotal,
                                    SpotPrice = buySubtotal / buyAmount,
                                    TotalFee = totalFee
                                };

                                currencyDict.AllTimeInvested += buySubtotal;
                                currencyDict.AllTimeCosts += buyCost;
                                currencyDict.AllTimeFees += totalFee;
                                
                                currencyDict.Orders.Add(buyOrder);
                                break;
                            }
                            case "sell":
                            {
                                var sellAmount = transaction.TransactionAmount;
                                
                                // sell price and fee
                                
                                
                                
                                break;
                            }
                        }
                    }
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}