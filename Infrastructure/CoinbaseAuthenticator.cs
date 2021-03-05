using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public class CoinbaseAuthenticator : ICoinbaseAuthenticator
    {
        public CoinbaseAuthenticator()
        {
            
        }
        
        /// <summary>
        /// Creates signature for signing requests for the api
        /// https://developers.coinbase.com/docs/wallet/api-key-authentication
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="httpMethod"></param>
        /// <param name="requestPath"></param>
        /// <param name="apiSecret"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public string CreateSignature(double timestamp, HttpMethod httpMethod, string requestPath, string apiSecret, string body = "")
        {
            var secretBytes = Encoding.UTF8.GetBytes(apiSecret);
            var prehash = timestamp.ToString("F0", CultureInfo.InvariantCulture) + httpMethod.ToString().ToUpper() +
                          requestPath + body;
            return ByteArrayToHex(prehash, secretBytes);
        }
        
        /// <summary>
        /// https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        /// </summary>
        private static string ByteArrayToHex(string str, byte[] secret)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            using var hmaccsha = new HMACSHA256(secret);
            return Convert.ToBase64String(hmaccsha.ComputeHash(bytes));
        }
    }
}