using System.Threading;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Coinbase_Portfolio_Tracker.Services.Google;
using Microsoft.Extensions.Hosting;

namespace Coinbase_Portfolio_Tracker
{
    public class App : IHostedService
    {
        private readonly ICoinbasePerformanceService _coinbasePerformanceService;
        private readonly IGoogleSpreadsheetService _googleSpreadsheetService;
        
        public App(ICoinbasePerformanceService coinbasePerformanceService,
            IGoogleSpreadsheetService googleSpreadsheetService)
        {
            _coinbasePerformanceService = coinbasePerformanceService;
            _googleSpreadsheetService = googleSpreadsheetService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Connect to coinbase and grab account info
            //var myCoinbase = await _coinbasePerformanceService.GetCoinbasePerformanceAsync();
            
            // Connect to google spreadsheets and fill info from above
            var googleSheetService = _googleSpreadsheetService.GetSpreadsheetService();
            await _googleSpreadsheetService.ReadAsync(googleSheetService.Spreadsheets.Values);
            // Display results
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}