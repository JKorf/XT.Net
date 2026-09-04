using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of XT
    /// </summary>
    public interface IXTSharedApiClient
    {
        /// <summary>
        /// Spot REST shared API implementations
        /// </summary>
        IXTRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// USDT-M Futures REST shared API implementations
        /// </summary>
        IXTRestClientFuturesSharedApi UsdtFuturesRest { get; }

        /// <summary>
        /// Coin-M Futures REST shared API implementations
        /// </summary>
        IXTRestClientFuturesSharedApi CoinFuturesRest { get; }

        /// <summary>
        /// Spot WebSocket shared API implementations
        /// </summary>
        IXTSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures WebSocket shared API implementations
        /// </summary>
        IXTSocketClientFuturesSharedApi FuturesSocket { get; }
    }
}
