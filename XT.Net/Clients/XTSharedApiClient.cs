using XT.Net.Interfaces.Clients;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Clients
{
    /// <inheritdoc />
    public class XTSharedApiClient : IXTSharedApiClient
    {
        /// <inheritdoc />
        public IXTRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IXTRestClientFuturesSharedApi UsdtFuturesRest { get; }
        /// <inheritdoc />
        public IXTRestClientFuturesSharedApi CoinFuturesRest { get; }
        /// <inheritdoc />
        public IXTSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IXTSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public XTSharedApiClient(
            IXTRestClient restClient,
            IXTSocketClient socketClient)
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdtFuturesRest = restClient.UsdtFuturesApi.SharedApi;
            CoinFuturesRest = restClient.CoinFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
