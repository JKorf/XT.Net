using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;
using XT.Net.Objects.Options;
using XT.Net.Objects.Sockets;
using XT.Net.Objects.Sockets.Subscriptions;

namespace XT.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the XT Futures websocket Api
    /// </summary>
    internal partial class XTSocketClientFuturesApi : SocketApiClient, IXTSocketClientFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _eventPath = MessagePath.Get().Property("event");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientFuturesApi(ILogger logger, XTSocketOptions options) :
            base(logger, options.Environment.FuturesSocketClientAddress!, options, options.FuturesOptions)
        {
            RegisterPeriodicQuery("Ping", TimeSpan.FromSeconds(20), x => new XTPingQuery(), null);
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTFuturesAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTFuturesTrade>(_logger,
                symbols.Select(x => "trade@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new XTSubscription<XTFuturesKline>(_logger,
                symbols.Select(x => "kline@" + x.ToLower() + "," + intervalStr).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTFuturesTicker>(_logger,
                symbols.Select(x => "ticker@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
            => await SubscribeToAggregatedTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTMarketInfo>(_logger,
                symbols.Select(x => "agg_ticker@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToIndexPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTPrice>(_logger,
                symbols.Select(x => "index_price@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTPrice>(_logger,
                symbols.Select(x => "mark_price@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTFuturesIncrementalOrderBookUpdate>(_logger,
                symbols.Select(x => "depth_update@" + x.ToLower() + "," + (updateInterval ?? 100) + "ms").ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTFuturesOrderBookUpdate>(_logger,
                symbols.Select(x => "depth@" + x.ToLower() + "," + depth + "," + (updateInterval ?? 100) + "ms").ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false,
                null,
                symbols.Select(x => "depth@" + x.ToLower() + "," + depth).ToArray());
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToFundingRateUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTFundingRateUpdate>(_logger,
                symbols.Select(x => "mark_price@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTFuturesAuthSubscription<XTFuturesBalanceUpdate>(_logger,
                "balance",
                listenKey,
                onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTFuturesAuthSubscription<XTFuturesPositionUpdate>(_logger,
                "position",
                listenKey,
                onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTFuturesAuthSubscription<XTFuturesOrder>(_logger,
                "order",
                listenKey,
                onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTFuturesAuthSubscription<XTFuturesUserTradeUpdate>(_logger,
                "trade",
                listenKey,
                onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(string listenKey, Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTFuturesAuthSubscription<XTNotification>(_logger,
                "notify",
                listenKey,
                onMessage);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsJson)
                return message.GetOriginalString();

            var id = message.GetValue<string?>(_idPath);
            if (id != null)
                return id;

            return message.GetValue<string>(_eventPath);
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public IXTSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
