using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            
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

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}