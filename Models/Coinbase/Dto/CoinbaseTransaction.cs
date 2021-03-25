using System;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Dto
{
    public class CoinbaseTransaction
    {
        public string TransactionId { get; set; }
        public string Type { get; set; }
        public decimal TransactionAmount { get; set; }
        public string TransactionAmountCurrency { get; set; }
        public decimal TransactionNativeAmount { get; set; }
        public string TransactionNativeAmountCurrency { get; set; }
        public DateTimeOffset? TransactionCreatedDate { get; set; }
        public CoinbaseBuyTransaction Buy { get; set; }
        public CoinbaseSellTransaction Sell { get; set; }
    }

    public class CoinbaseBuyTransaction
    {
        public string Id { get; set; }
        public string Resource { get; set; }
        public string ResourcePath { get; set; }
    }
    
    public class CoinbaseSellTransaction
    {
        public string Id { get; set; }
        public string Resource { get; set; }
        public string ResourcePath { get; set; }
    }
    
}