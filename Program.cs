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