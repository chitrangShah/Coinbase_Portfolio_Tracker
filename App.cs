using System.Threading;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Coinbase_Portfolio_Tracker.Services;
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
        private readonly ICoinbaseSellService _coinbaseSellService;

        public App(ICoinbaseAccountService coinbaseAccountService,
            ICoinbaseSpotPriceService coinbaseSpotPriceService,
            ICoinbaseTransactionService coinbaseTransactionService,
            ICoinbaseBuyService coinbaseBuyService,
            ICoinbaseSellService coinbaseSellService)
        {
            _coinbaseAccountService = coinbaseAccountService;
            _coinbaseSpotPriceService = coinbaseSpotPriceService;
            _coinbaseTransactionService = coinbaseTransactionService;
            _coinbaseBuyService = coinbaseBuyService;
            _coinbaseSellService = coinbaseSellService;
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
                                var buy = await _coinbaseBuyService.GetBuyAsync(account.Id, transaction.Buy.Id);
                                var buyCost = buy.TotalAmount;
                                var buySubtotal = buy.SubtotalAmount;
                                var buyTotalFee = buy.FeeAmount;

                                var buyOrder = new BuyOrder()
                                {
                                    Type = "buy",
                                    Datetime = transaction.TransactionCreatedDate,
                                    Symbol = transaction.TransactionAmountCurrency,
                                    Amount = buyAmount,
                                    Cost = buyCost,
                                    Invested = buySubtotal,
                                    SpotPrice = buySubtotal / buyAmount,
                                    TotalFee = buyTotalFee
                                };

                                currencyDict.AllTimeInvested += buySubtotal;
                                currencyDict.AllTimeCosts += buyCost;
                                currencyDict.AllTimeFees += buyTotalFee;
                                
                                currencyDict.Orders.Add(buyOrder);
                                break;
                            }
                            case "sell":
                            {
                                var sellAmount = transaction.TransactionAmount;
                                
                                // sell price and fee
                                var sell = await _coinbaseSellService.GetSellAsync(account.Id, transaction.Sell.Id);
                                var sellEarned = sell.TotalAmount;
                                var sellTotal = sell.SubtotalAmount;
                                var sellTotalFee = sell.FeeAmount;

                                var sellOrder = new SellOrder()
                                {
                                    Type = "sell",
                                    Datetime = transaction.TransactionCreatedDate,
                                    Symbol = transaction.TransactionAmountCurrency,
                                    Amount = sellAmount,
                                    Earned = sellEarned,
                                    SellTotal = sellTotal,
                                    SpotPrice = -sellTotal / sellAmount,
                                    TotalFee = sellTotalFee
                                };

                                currencyDict.AllTimeFees += sellTotalFee;
                                
                                currencyDict.Orders.Add(sellOrder);
                                break;
                            }
                        }
                    }
                    
                    // sort orders by date
                    currencyDict.Orders
                        .Sort((a,b) =>
                        {
                            if (a.Datetime == null) 
                                return 0;
                            return b.Datetime != null ? a.Datetime.Value.CompareTo(b.Datetime.Value) : 0;
                        });
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}