using System.Collections.Generic;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker
{
    public class App
    {
        private readonly IConfiguration _configuration;
        private readonly ICoinbaseAccountService _coinbaseAccountService;
        private readonly ICoinbaseSpotPriceService _coinbaseSpotPriceService;

        public App(IConfiguration configuration,
            ICoinbaseAccountService coinbaseAccountService,
            ICoinbaseSpotPriceService coinbaseSpotPriceService)
        {
            _configuration = configuration;
            _coinbaseAccountService = coinbaseAccountService;
            _coinbaseSpotPriceService = coinbaseSpotPriceService;
        }
        
        public async Task Run()
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
            }
            // ** Google Api Steps **
            // Connect to spreadsheet
            // Fill out 1st sheet, Portfolio Overview
            // Fill out 2nd sheet, Wallet Details
            // Fill out 3rd sheet, Order Details

            // ** Display results **
            // Create spreadsheet url
            // Display in console or send via email
        }
    }
}