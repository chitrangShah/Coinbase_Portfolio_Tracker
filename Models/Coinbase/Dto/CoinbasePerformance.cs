using System;
using System.Collections.Generic;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Dto
{
    public class CoinbasePerformance
    {
        public decimal CurrentValue { get; set; }
        public decimal CurrentUnrealizedGain { get; set; }
        public decimal CurrentPerformance { get; set; }
        public List<CoinbaseCurrency> Currencies { get; set; }
    }

    public class CoinbaseCurrency
    {
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public CoinbasePrice CurrentPrice { get; set; }
        public decimal CurrentTotal { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal OriginalWorth { get; set; }
        public decimal SellOriginalWorth { get; set; }
        public decimal RealizedGainLoss { get; set; }
        public decimal UnrealizedGainLoss { get; set; }
        public decimal CurrentPerformance { get; set; }
        public decimal RealizedGainPerformance { get; set; }
        public decimal AllTimeInvested { get; set; }
        public decimal AllTimeFees { get; set; }
        public decimal AllTimeCosts { get; set; }
        public List<ICoinbaseOrder> Orders { get; set; }
    }

    public interface ICoinbaseOrder
    {
        string Type { get; set; }
        DateTimeOffset? Datetime { get; set; }
        string Symbol { get; set; }
        decimal Amount { get; set; }
        decimal OriginalWorth { get; set; }
    }

    public class BuyOrder : ICoinbaseOrder
    {
        public string Type { get; set; }
        public DateTimeOffset? Datetime { get; set; }
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal OriginalWorth { get; set; }
        public decimal Cost { get; set; }
        public decimal Invested { get; set; }
        public decimal SpotPrice { get; set; }
        public decimal TotalFee { get; set; }
    }

    public class SellOrder : ICoinbaseOrder
    {
        public string Type { get; set; }
        public DateTimeOffset? Datetime { get; set; }
        public string Symbol { get; set; }
        public decimal Amount { get; set; }
        public decimal OriginalWorth { get; set; }
        public decimal Earned { get; set; }
        public decimal SellTotal { get; set; }
        public decimal SpotPrice { get; set; }
        public decimal TotalFee { get; set; }
    }
}