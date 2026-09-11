using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// XT options
    /// </summary>
    public class XTOptions : LibraryOptions<XTRestOptions, XTSocketOptions, XTCredentials, XTEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public SharedApiOptions SharedApi { get; set; } = new();
    }
}
