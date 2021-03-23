using System;
using System.Reflection;
using Coinbase_Portfolio_Tracker.Models.Coinbase;
using Coinbase_Portfolio_Tracker.Models.Coinbase.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            if (reader.TokenType != JsonToken.StartObject) 
                return new JsonException("No properties found for read");
            
            JObject item = JObject.Load(reader);

            if (item["type"] == null)
                return new JsonException("No properties found for read");

            var transactionType = item["type"].Value<string>();
            
            switch (transactionType)
            {
                case "buy":
                    var buyResponse = new CoinbaseTransactionBuyResponseDetails();
                    serializer.Populate(item.CreateReader(), buyResponse);
                    return buyResponse;
                case "sell":
                    var sellResponse = new CoinbaseTransactionSellResponseDetails();
                    serializer.Populate(item.CreateReader(), sellResponse);
                    return sellResponse;
                default:
                    throw new JsonException("");
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(CoinbaseTransactionResponseDetails)
                .GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }
    }
}