using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using XT.Net.Clients.MessageHandlers;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Internal;
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
            ProcessUnparsableMessages = true;

            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(20),
                x => new XTPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.ErrorType == ErrorType.Timeout)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(XTExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new XTSocketMessageHandler();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTFuturesAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesTrade>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesTrade>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTFuturesTrade>(_logger,
                this,
                "trade",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesKline>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesKline>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new XTSubscription<XTFuturesKline>(_logger,
                this,
                "kline",
                symbols.Select(x => x.ToLowerInvariant() + "," + intervalStr).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesTicker>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesTicker>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTFuturesTicker>(_logger,
                this,
                "ticker",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
            => await SubscribeToAggregatedTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTMarketInfo>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTMarketInfo>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTMarketInfo>(_logger,
                this,
                "agg_ticker",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToIndexPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTPrice>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTPrice>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTPrice>(_logger,
                this,
                "index_price",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTPrice>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTPrice>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTPrice>(_logger,
                this,
                "mark_price",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesIncrementalOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesIncrementalOrderBookUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTFuturesIncrementalOrderBookUpdate>(_logger,
                this,
                "depth_update",
                symbols.Select(x => x.ToLowerInvariant() + "," + (updateInterval ?? 100) + "ms").ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesOrderBookUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTFuturesOrderBookUpdate>(_logger,
                this,
                "depth",
                symbols.Select(x => x.ToLowerInvariant() + "," + depth + "," + (updateInterval ?? 100) + "ms").ToArray(),
                internalHandler,
                false,
                null,
                symbols.Select(x => "depth@" + x.ToLowerInvariant() + "," + depth).ToArray());
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToFundingRateUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFundingRateUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFundingRateUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTFundingRateUpdate>(_logger,
                this,
                "mark_price",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/market"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesBalanceUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesBalanceUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesBalanceUpdate>(_logger,
                this,
                "balance",
                listenKey,
                internalHandler);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesPositionUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesPositionUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesPositionUpdate>(_logger,
                this,
                "position",
                listenKey,
                internalHandler);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesOrder>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesOrder>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesOrder>(_logger,
                this,
                "order",
                listenKey,
                internalHandler);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesUserTradeUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesUserTradeUpdate>(_logger,
                this,
                "trade",
                listenKey,
                internalHandler);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(string listenKey, Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTNotification>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTNotification>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTNotification>(_logger,
                this,
                "notify",
                listenKey,
                internalHandler);
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsValid)
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
