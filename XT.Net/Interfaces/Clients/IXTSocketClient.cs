using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the XT websocket API
    /// </summary>
    public interface IXTSocketClient : ISocketClient<XTCredentials>
    {

        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IXTSocketClientFuturesApi"/>
        public IXTSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IXTSocketClientSpotApi"/>
        public IXTSocketClientSpotApi SpotApi { get; }
    }
}
