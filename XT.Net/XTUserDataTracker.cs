using XT.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace XT.Net
{
    /// <inheritdoc/>
    public class XTUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public XTUserSpotDataTracker(
            ILogger<XTUserSpotDataTracker> logger,
            IXTRestClient restClient,
            IXTSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                restClient.SpotApi.SharedApi,
                socketClient.SpotApi.SharedApi,

                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class XTUserUsdtFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public XTUserUsdtFuturesDataTracker(
            ILogger<XTUserUsdtFuturesDataTracker> logger,
            IXTRestClient restClient,
            IXTSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.UsdtFuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                restClient.UsdtFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.UsdtFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {
        }
    }

    /// <inheritdoc/>
    public class XTUserCoinFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc/>
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public XTUserCoinFuturesDataTracker(
            ILogger<XTUserCoinFuturesDataTracker> logger,
            IXTRestClient restClient,
            IXTSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig config) : base(logger,
                restClient.CoinFuturesApi.SharedApi,

                restClient.CoinFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.CoinFuturesApi.SharedApi,
                restClient.CoinFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.CoinFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,

                restClient.CoinFuturesApi.SharedApi,
                socketClient.FuturesApi.SharedApi,
                userIdentifier,
                config)
        {
        }
    }
}
