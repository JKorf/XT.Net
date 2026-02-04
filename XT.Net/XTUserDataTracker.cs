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
            SpotUserDataTrackerConfig config) : base(
                logger,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                userIdentifier,
                config)
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
            FuturesUserDataTrackerConfig config) : base(logger,
                restClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                restClient.UsdtFuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                userIdentifier,
                config)
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
                restClient.CoinFuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                restClient.CoinFuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                userIdentifier,
                config)
        {
        }
    }
}
