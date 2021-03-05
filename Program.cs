using System;
using System.IO;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Infrastructure;
using Coinbase_Portfolio_Tracker.Models.Config;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coinbase_Portfolio_Tracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Configure services
            var services = ConfigureServices();
            
            // Generate provider
            var serviceProvider = services.BuildServiceProvider();
            
            // Start app
            await serviceProvider.GetService<App>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();
            
            // Configuration settings
            var config = LoadConfiguration();
            services.AddSingleton(config);
            
            // Appsettings 
            services.Configure<CoinbaseOptions>(config.GetSection(CoinbaseOptions.SectionName));
            
            // Api services
            services.AddTransient<ICoinbaseAuthenticator, CoinbaseAuthenticator>();
            services.AddTransient<ICoinbaseConnectService, CoinbaseConnectService>();
            services.AddTransient<ICoinbaseAccountService, CoinbaseAccountService>();
            services.AddTransient<ICoinbaseSpotPriceService, CoinbaseSpotPriceService>();
            
            // Register App entry
            services.AddTransient<App>();

            return services;
        }

        private static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}