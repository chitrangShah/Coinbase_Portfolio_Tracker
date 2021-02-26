using System;

namespace Coinbase_Portfolio_Tracker.Api.Models.Coinbase.Dto
{
    public class CoinbaseTransaction
    {
        public string Type { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionAmountCurrency { get; set; }
        public decimal TransactionNativeAmount { get; set; }
        public string TransactionNativeAmountCurrency { get; set; }
        public DateTime TransactionCreatedDate { get; set; }
    }
}