using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the XT Rest API. 
    /// </summary>
    public interface IXTRestClient : IRestClient<XTCredentials>
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
    }
}
