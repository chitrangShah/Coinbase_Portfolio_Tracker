namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Dto
{
    public class CoinbaseSell
    {
        public decimal SellAmount { get; set; }
        public string SellAmountCurrency { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountCurrency { get; set; }
        public decimal SubtotalAmount { get; set; }
        public string SubtotalAmountCurrency { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeeAmountCurrency { get; set; }
    }
}