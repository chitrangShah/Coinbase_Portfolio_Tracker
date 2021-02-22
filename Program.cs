using System;
using Coinbase_Portfolio_Tracker.Api.Config;
using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = InitOptions<AppConfig>();
            
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
        
        private static T InitOptions<T>()
            where T : new()
        {
            var config = InitConfig();
            return config.Get<T>();
        }
        
        private static IConfigurationRoot InitConfig()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env}.json", true, true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}