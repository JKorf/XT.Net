using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot API endpoints
    /// </summary>
    public interface IXTRestClientSpotApi : IRestApiClient<XTCredentials>, IDisposable
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
        /// [V1] Get the shared rest requests client. For new implementations prefer <see cref="SharedApi"/>
        /// </summary>
        public IXTRestClientSpotApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IXTRestClientSpotSharedApi SharedApi { get; }
    }
}
