using CryptoExchange.Net.Interfaces;
using System;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures API endpoints
    /// </summary>
    public interface IXTRestClientFuturesApi : IRestApiClient, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IXTRestClientFuturesApiAccount"/>
        public IXTRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IXTRestClientFuturesApiExchangeData"/>
        public IXTRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IXTRestClientFuturesApiTrading"/>
        public IXTRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IXTRestClientFuturesApiShared SharedClient { get; }
    }
}
