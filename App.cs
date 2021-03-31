using System.Threading;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Hosting;

namespace Coinbase_Portfolio_Tracker
{
    public class App : IHostedService
    {
        private readonly ICoinbasePerformanceService _coinbasePerformanceService;
        
        public App(ICoinbasePerformanceService coinbasePerformanceService)
        {
            _coinbasePerformanceService = coinbasePerformanceService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Connect to coinbase and grab account info
            var myCoinbase = await _coinbasePerformanceService.GetCoinbasePerformanceAsync();
            
            // Connect to google spreadsheets and fill info from above
            
            // Display results
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}