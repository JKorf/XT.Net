using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using XT.Net.Clients;
using XT.Net.Interfaces;
using XT.Net.Interfaces.Clients;

namespace XT.Net
{
    /// <inheritdoc />
    public class XTTrackerFactory : IXTTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public XTTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public XTTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = (_serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient());
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.FuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval);
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IXTRestClient>() ?? new XTRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.CoinFuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }
        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IXTRestClient>() ?? new XTRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else if (symbol.TradingMode.IsLinear())
            {
                sharedRestClient = restClient.UsdtFuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.CoinFuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IXTRestClient>() ?? new XTRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient();
            return new XTUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<XTUserSpotDataTracker>>() ?? new NullLogger<XTUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, SpotUserDataTrackerConfig config, ApiCredentials credentials, XTEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IXTUserClientProvider>() ?? new XTUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new XTUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<XTUserSpotDataTracker>>() ?? new NullLogger<XTUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(FuturesUserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IXTRestClient>() ?? new XTRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient();
            return new XTUserUsdtFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<XTUserUsdtFuturesDataTracker>>() ?? new NullLogger<XTUserUsdtFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(string userIdentifier, FuturesUserDataTrackerConfig config, ApiCredentials credentials, XTEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IXTUserClientProvider>() ?? new XTUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new XTUserUsdtFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<XTUserUsdtFuturesDataTracker>>() ?? new NullLogger<XTUserUsdtFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }


        // Coin futures doesn't work correctly ATM

        ///// <inheritdoc />
        //public IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(FuturesUserDataTrackerConfig config)
        //{
        //    var restClient = _serviceProvider?.GetRequiredService<IXTRestClient>() ?? new XTRestClient();
        //    var socketClient = _serviceProvider?.GetRequiredService<IXTSocketClient>() ?? new XTSocketClient();
        //    return new XTUserCoinFuturesDataTracker(
        //        _serviceProvider?.GetRequiredService<ILogger<XTUserCoinFuturesDataTracker>>() ?? new NullLogger<XTUserCoinFuturesDataTracker>(),
        //        restClient,
        //        socketClient,
        //        null,
        //        config
        //        );
        //}

        ///// <inheritdoc />
        //public IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(string userIdentifier, FuturesUserDataTrackerConfig config, ApiCredentials credentials, XTEnvironment? environment = null)
        //{
        //    var clientProvider = _serviceProvider?.GetRequiredService<IXTUserClientProvider>() ?? new XTUserClientProvider();
        //    var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
        //    var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
        //    return new XTUserCoinFuturesDataTracker(
        //        _serviceProvider?.GetRequiredService<ILogger<XTUserCoinFuturesDataTracker>>() ?? new NullLogger<XTUserCoinFuturesDataTracker>(),
        //        restClient,
        //        socketClient,
        //        userIdentifier,
        //        config
        //        );
        //}
    }
}
