using CryptoExchange.Net.Objects.Options;
using System;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// XT Spot Rest API options
    /// </summary>
    public class XTSpotRestApiOptions : RestApiOptions
    {
        /// <summary>
        /// The timespan for how long the server will regard a request as valid, it will reject the request if it's received after this time has passed
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        internal XTSpotRestApiOptions Set(XTSpotRestApiOptions targetOptions)
        {
            targetOptions = base.Set<XTSpotRestApiOptions>(targetOptions);
            targetOptions.ReceiveWindow = ReceiveWindow;
            return targetOptions;
        }
    }
}
