using System.IO;
using System.Threading.Tasks;
using Coinbase_Portfolio_Tracker.Shared;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Coinbase_Portfolio_Tracker.Services.Google
{
    public interface IGoogleSpreadsheetService
    {
        SheetsService GetSpreadsheetService();
        Task ReadAsync(SpreadsheetsResource.ValuesResource valuesResource);
        Task WriteAsync(SpreadsheetsResource.ValuesResource valuesResource);
    }

    public class GoogleSpreadsheetService : IGoogleSpreadsheetService
    {
        private static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };

        public SheetsService GetSpreadsheetService()
        {
            using var stream =
                new FileStream(Constants.GoogleSpreadsheetCredentialsFileName, FileMode.Open, FileAccess.Read);
            
            var serviceInitializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromStream(stream).CreateScoped(Scopes)
            };
            
            return new SheetsService(serviceInitializer);
        }

        public async Task ReadAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            var response = await valuesResource.Get(Constants.GoogleSpreadsheetId, "A:B").ExecuteAsync();
            var values = response.Values;
        }

        public async Task WriteAsync(SpreadsheetsResource.ValuesResource valuesResource)
        {
            throw new System.NotImplementedException();
        }
    }
}