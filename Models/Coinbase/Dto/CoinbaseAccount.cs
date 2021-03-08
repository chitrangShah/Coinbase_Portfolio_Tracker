namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Dto
{
    public class CoinbaseAccount
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountCurrency { get; set; }
        public decimal BalanceAmount { get; set; }
        public string BalanceAmountCurrency { get; set; }
    }
}