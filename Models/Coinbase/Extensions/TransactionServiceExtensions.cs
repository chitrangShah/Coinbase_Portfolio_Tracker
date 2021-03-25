using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;

namespace Coinbase_Portfolio_Tracker.Models.Coinbase.Extensions
{
    public static class TransactionServiceExtensions
    {
        public static CoinbaseBuyTransaction ToBuyTransaction(
            this CoinbaseResourceResponseDetails coinbaseResourceResponseDetails)
        {
            if (coinbaseResourceResponseDetails == null)
                return null;
            
            return new CoinbaseBuyTransaction()
            {
                Id = coinbaseResourceResponseDetails.Id,
                Resource = coinbaseResourceResponseDetails.Resource,
                ResourcePath = coinbaseResourceResponseDetails.ResourcePath
            };
        }
        
        public static CoinbaseSellTransaction ToSellTransaction(
            this CoinbaseResourceResponseDetails coinbaseResourceResponseDetails)
        {
            if (coinbaseResourceResponseDetails == null)
                return null;
            
            return new CoinbaseSellTransaction()
            {
                Id = coinbaseResourceResponseDetails.Id,
                Resource = coinbaseResourceResponseDetails.Resource,
                ResourcePath = coinbaseResourceResponseDetails.ResourcePath
            };
        }
    }
}