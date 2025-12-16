using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot API endpoints
    /// </summary>
    public interface IXTRestClientSpotApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IXTRestClientSpotApiAccount"/>
        public IXTRestClientSpotApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IXTRestClientSpotApiExchangeData"/>
        public IXTRestClientSpotApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IXTRestClientSpotApiTrading"/>
        public IXTRestClientSpotApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IXTRestClientSpotApiShared SharedClient { get; }
    }
}
