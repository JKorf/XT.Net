using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;
using XT.Net.Interfaces.Clients;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Options;

namespace XT.Net.Clients
{
    /// <inheritdoc />
    public class XTSharedApiClient : SharedApiClientBase, IXTSharedApiClient
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
            IXTSocketClient socketClient,
            IOptions<XTOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,
                   restClient.SpotApi.SharedApi,
                   socketClient.SpotApi.SharedApi,
                   restClient.UsdtFuturesApi.SharedApi,
                   restClient.CoinFuturesApi.SharedApi,
                   socketClient.FuturesApi.SharedApi
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            UsdtFuturesRest = restClient.UsdtFuturesApi.SharedApi;
            CoinFuturesRest = restClient.CoinFuturesApi.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
        }
    }
}
