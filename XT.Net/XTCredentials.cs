using CryptoExchange.Net.Authentication;

namespace XT.Net
{
    /// <summary>
    /// XT credentials
    /// </summary>
    public class XTCredentials : ApiCredentials
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="apiKey">The API key</param>
        /// <param name="secret">The API secret</param>
        public XTCredentials(string apiKey, string secret) : this(new HMACCredential(apiKey, secret)) { }
       
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="credential">The HMAC credentials</param>
        public XTCredentials(HMACCredential credential) : base(credential) { }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new XTCredentials(Hmac!);
    }
}
