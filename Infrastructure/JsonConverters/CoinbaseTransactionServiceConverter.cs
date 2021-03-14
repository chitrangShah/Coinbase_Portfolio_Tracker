using System;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Dto;
using Newtonsoft.Json;

namespace Coinbase_Portfolio_Tracker.Infrastructure.JsonConverters
{
    public class CoinbaseTransactionServiceConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            
            // start object
            reader.Read();
            
            //we should be at geometry type property now
            if ((string)reader.Value != "type") 
                throw new InvalidOperationException();
            
            reader.Read();
            
            var type = (string)reader.Value;

            CoinbaseTransactionResponseDetails value = type switch
            {
                "buy" => new CoinbaseTransactionBuyResponseDetails(),
                "sell" => new CoinbaseTransactionSellResponseDetails(),
                _ => throw new NotSupportedException()
            };
            
            // move to inner object property
            //should probably confirm name here
            reader.Read();

            //move to inner object
            reader.Read();
            
            serializer.Populate(reader, value);
            
            //move outside container (should be end object)
            reader.Read();
            
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(CoinbaseTransaction).IsAssignableFrom(objectType);
        }
    }
}