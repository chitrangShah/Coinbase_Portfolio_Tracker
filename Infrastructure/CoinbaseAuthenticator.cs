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
        /// <param name="requestPath">AbsolutePath only, eg: /v2/accounts
        /// Do not include full url such as https://api.coinbase.com/v2/accounts, will generate auth error</param>
        /// <param name="apiSecret"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public string CreateSignature(string timestamp, HttpMethod httpMethod, string requestPath, string apiSecret, string body = "")
        {
            var secretKey = Encoding.UTF8.GetBytes(apiSecret);
            var prehash = Encoding.UTF8.GetBytes(timestamp + httpMethod.Method.ToUpperInvariant() + requestPath + body);

            using( var hmac = new HMACSHA256(secretKey) )
            {
                var signature = hmac.ComputeHash(prehash);
                
                // return hex string
                return ByteArrayToHex(signature);
            }
        }
        
        /// <summary>
        /// https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa
        /// </summary>
        private static string ByteArrayToHex(byte[] barray)
        {
            char[] c = new char[barray.Length * 2];
            int b;
            for (int i = 0; i < barray.Length; i++)
            {
                b = barray[i] >> 4;
                c[i * 2] = (char)(87 + b + (((b - 10) >> 31) & -39));
                b = barray[i] & 0xF;
                c[i * 2 + 1] = (char)(87 + b + (((b - 10) >> 31) & -39));
            }
            return new string(c);
        }
    }
}