namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase.Dto
{
    public class CoinbaseAccount
    {
        public string AccountCurrency { get; set; }
        public decimal BalanceAmount { get; set; }
        public string BalanceAmountCurrency { get; set; }
    }
}