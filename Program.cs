using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Infrastructure;
using Coinbase_Portfolio_Tracker.Models.Config;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Coinbase_Portfolio_Tracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var env = builderContext.HostingEnvironment;
                    
                    config.SetBasePath(env.ContentRootPath);

                    config.AddJsonFile(
                        "appsettings.json",
                        optional: false, reloadOnChange: true);
                    config.AddJsonFile(
                        $"appsettings.{env.EnvironmentName}.json",
                        optional: true, reloadOnChange: true);
                    
                    // Added before AddUserSecrets to let user secrets override
                    // environment variables.
                    config.AddEnvironmentVariables();
                    
                    // Add user secrets only for development
                    if (!env.IsDevelopment()) 
                        return;
                    
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    config.AddUserSecrets(appAssembly, true);
                })
                .ConfigureLogging(logging =>
                {
                    // Add logging framework
                    
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .AddHostedService<App>()
                        .AddTransient<ICoinbaseAuthenticator, CoinbaseAuthenticator>()
                        .AddTransient<ICoinbaseConnectService, CoinbaseConnectService>()
                        .AddTransient<ICoinbaseAccountService, CoinbaseAccountService>()
                        .AddTransient<ICoinbaseSpotPriceService, CoinbaseSpotPriceService>()
                        .AddTransient<ICoinbaseTransactionService, CoinbaseTransactionService>();

                    services.AddOptions<CoinbaseOptions>()
                        .Bind(hostContext.Configuration.GetSection(CoinbaseOptions.SectionName));
                })
                .RunConsoleAsync();
        }
    }
}