using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Hosting;

namespace Coinbase_Portfolio_Tracker
{
    public class App : IHostedService
    {
        private readonly ICoinbaseAccountService _coinbaseAccountService;
        private readonly ICoinbaseSpotPriceService _coinbaseSpotPriceService;
        private readonly ICoinbaseTransactionService _coinbaseTransactionService;

        public App(ICoinbaseAccountService coinbaseAccountService,
            ICoinbaseSpotPriceService coinbaseSpotPriceService,
            ICoinbaseTransactionService coinbaseTransactionService)
        {
            _coinbaseAccountService = coinbaseAccountService;
            _coinbaseSpotPriceService = coinbaseSpotPriceService;
            _coinbaseTransactionService = coinbaseTransactionService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var myCoinbasePerformance = new Dictionary<string, dynamic>()
            {
                {"current_value", 0},
                {"current_unrealized_gain", 0},
                {"current_performance", 0},
                {"currencies", new Dictionary<string, dynamic>()}
            };

            // ** Coinbase Api Steps **
            // Create service client to connect to coinbase
            // Get account info
            var accounts = await _coinbaseAccountService.GetAllAccountsAsync();

            foreach (var account in accounts)
            {
                if (account.AccountCurrency == "USD" || account.BalanceAmount == 0)
                {
                    // Do nothing
                }
                // Get current amount and totals
                else
                {
                    var currentSpotPrice = await _coinbaseSpotPriceService
                        .GetSpotPriceAsync(account.AccountCurrency + "-USD");

                    var currencyDict = new Dictionary<string, dynamic>()
                    {
                        {"symbol", account.AccountCurrency},
                        {"quantity", account.BalanceAmount},
                        {"current_price", currentSpotPrice},
                        {"current_total", account.BalanceAmount},
                        {"average_price", 0},
                        {"original_worth", 0},
                        {"sell_original_worth", 0},
                        {"realized_gain_loss", 0},
                        {"unrealized_gain_loss", 0},
                        {"current_performance", 0},
                        {"realized_gain_performance", 0},
                        {"all_time_invested", 0},
                        {"all_time_costs", 0},
                        {"all_time_fees", 0},
                        {"orders", new List<string>()}
                    };

                    myCoinbasePerformance["currencies"].Add(currencyDict);
                }

                // Get transactions
                var transactions = await _coinbaseTransactionService.GetAllTransactionsAsync(account.Id);

                foreach (var transaction in transactions)
                {
                    // Buys

                    // Sells
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}