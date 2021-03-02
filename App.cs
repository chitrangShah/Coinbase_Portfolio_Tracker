using Microsoft.Extensions.Configuration;

namespace Coinbase_Portfolio_Tracker
{
    public class App
    {
        private readonly IConfiguration _configuration;

        public App(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void Run()
        {
            // TODO: Add api logic
        }
    }
}