using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the XT Rest API. 
    /// </summary>
    public interface IXTRestClient : IRestClient
    {
        /// <summary>
        /// USDT-M Futures API endpoints
        /// </summary>
        /// <see cref="IXTRestClientFuturesApi"/>
        public IXTRestClientFuturesApi UsdtFuturesApi { get; }
        /// <summary>
        /// Coin-M Futures API endpoints
        /// </summary>
        /// <see cref="IXTRestClientFuturesApi"/>
        public IXTRestClientFuturesApi CoinFuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IXTRestClientSpotApi"/>
        public IXTRestClientSpotApi SpotApi { get; }

        /// <summary>
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
