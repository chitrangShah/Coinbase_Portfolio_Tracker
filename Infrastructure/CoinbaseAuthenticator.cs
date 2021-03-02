using System;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Coinbase_Portfolio_Tracker.Infrastructure
{
    public class CoinbaseAuthenticator : ICoinbaseAuthenticator
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }

        public CoinbaseAuthenticator(string apiKey, string apiSecret)
        {
            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(apiSecret))
            {
                throw new ArgumentException(
                    $"{nameof(CoinbaseAuthenticator)} requires {nameof(apiKey)} and {nameof(apiSecret)}");
            }

            ApiKey = apiKey;
            ApiSecret = apiSecret;
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
            byte[] secretKey = Encoding.UTF8.GetBytes(apiSecret);
            HMACSHA256 hmac = new HMACSHA256(secretKey);
            hmac.Initialize();
            var message = timestamp.ToString("F0", CultureInfo.InvariantCulture) + httpMethod.ToString().ToUpper() + requestPath + body;
            var bytes = Encoding.UTF8.GetBytes(message);
            
            byte[] rawHmac = hmac.ComputeHash(bytes);
            var signature = ByteArrayToHex(rawHmac);

            return signature;
        }
        
        /// <summary>
        /// https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        /// </summary>
        private static string ByteArrayToHex(byte[] barray)
        {
            char[] c = new char[barray.Length * 2];
            byte b;
            for (int i = 0; i < barray.Length; ++i)
            {
                b = ((byte)(barray[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(barray[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }
    }
}