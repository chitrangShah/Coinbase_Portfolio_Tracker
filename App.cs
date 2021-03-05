using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker
{
    public class App
    {
        private readonly IConfiguration _configuration;
        private readonly ICoinbaseAccountService _coinbaseAccountService;

        public App(IConfiguration configuration,
            ICoinbaseAccountService coinbaseAccountService)
        {
            _configuration = configuration;
            _coinbaseAccountService = coinbaseAccountService;
        }
        
        public async Task Run()
        {
            // ** Coinbase Api Steps **
            // Create service client to connect to coinbase
            // Get account info
            var accountInfo = await _coinbaseAccountService.GetAccountInfo();

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