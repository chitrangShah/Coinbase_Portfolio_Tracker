namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase.Dto
{
    public class CoinbaseBuy
    {
        public decimal BuyAmount { get; set; }
        public string BuyAmountCurrency { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountCurrency { get; set; }
        public decimal SubtotalAmount { get; set; }
        public string SubtotalAmountCurrency { get; set; }
        public decimal FeeAmount { get; set; }
        public string FeeAmountCurrency { get; set; }
    }
}