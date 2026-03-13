using CryptoExchange.Net.Objects.Options;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// Options for the XTSocketClient
    /// </summary>
    public class XTSocketOptions : SocketExchangeOptions<XTEnvironment, XTCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static XTSocketOptions Default { get; set; } = new XTSocketOptions()
        {
            Environment = XTEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public XTSocketOptions()
        {
            Default?.Set(this);
        }

         /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions<XTCredentials> FuturesOptions { get; private set; } = new SocketApiOptions<XTCredentials>();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions<XTCredentials> SpotOptions { get; private set; } = new SocketApiOptions<XTCredentials>();

        internal XTSocketOptions Set(XTSocketOptions targetOptions)
        {
            targetOptions = base.Set<XTSocketOptions>(targetOptions);            
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
