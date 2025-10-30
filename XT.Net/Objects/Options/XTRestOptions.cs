using CryptoExchange.Net.Objects.Options;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// Options for the XTRestClient
    /// </summary>
    public class XTRestOptions : RestExchangeOptions<XTEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static XTRestOptions Default { get; set; } = new XTRestOptions()
        {
            Environment = XTEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public XTRestOptions()
        {
            Default?.Set(this);
        }
        
        /// <summary>
        /// Broker reference
        /// </summary>
        public string? BrokerId { get; set; }

         /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

         /// <summary>
        /// Spot API options
        /// </summary>
        public XTSpotRestApiOptions SpotOptions { get; private set; } = new XTSpotRestApiOptions();

        internal XTRestOptions Set(XTRestOptions targetOptions)
        {
            targetOptions = base.Set<XTRestOptions>(targetOptions);            
            targetOptions.BrokerId = BrokerId;
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
