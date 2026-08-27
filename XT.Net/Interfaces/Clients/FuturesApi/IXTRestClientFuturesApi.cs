using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures API endpoints
    /// </summary>
    public interface IXTRestClientFuturesApi : IRestApiClient<XTCredentials>, IDisposable
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
        /// Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IXTRestClientFuturesApiShared SharedClient { get; }
        /// <summary>
        /// Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IXTRestClientFuturesSharedApi SharedApi { get; }
    }
}
