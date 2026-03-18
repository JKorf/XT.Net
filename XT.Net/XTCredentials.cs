using CryptoExchange.Net.Authentication;

namespace XT.Net
{
    /// <summary>
    /// XT API credentials
    /// </summary>
    public class XTCredentials : HMACCredential
    {
        /// <summary>
        /// Create new credentials providing only credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public XTCredentials(string key, string secret) : base(key, secret)
        {
        }
    }
}
