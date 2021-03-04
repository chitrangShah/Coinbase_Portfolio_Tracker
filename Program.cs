using System;
using System.IO;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Config;
using Coinbase_Portfolio_Tracker.Services.Coinbase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coinbase_Portfolio_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure services
            var services = ConfigureServices();
            
            // Generate provider
            var serviceProvider = services.BuildServiceProvider();
            
            // Start app
            serviceProvider.GetService<App>()?.Run();
            
            // ** Coinbase Api Steps **
            // Create service client to connect to coinbase
            // Get account info

            // ** Google Api Steps **
            // Connect to spreadsheet
            // Fill out 1st sheet, Portfolio Overview
            // Fill out 2nd sheet, Wallet Details
            // Fill out 3rd sheet, Order Details

            // ** Display results **
            // Create spreadsheet url
            // Display in console or send via email
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