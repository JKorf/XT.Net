using CryptoExchange.Net.Objects.Options;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// Options for the XTSocketClient
    /// </summary>
    public class XTSocketOptions : SocketExchangeOptions<XTEnvironment>
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
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();


        internal XTSocketOptions Set(XTSocketOptions targetOptions)
        {
            targetOptions = base.Set<XTSocketOptions>(targetOptions);
            
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);

            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);

            return targetOptions;
        }
    }
}
