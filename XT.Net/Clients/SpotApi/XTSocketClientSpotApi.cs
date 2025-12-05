using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
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
using XT.Net.Clients.MessageHandlers;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;
using XT.Net.Objects.Options;
using XT.Net.Objects.Sockets;
using XT.Net.Objects.Sockets.Subscriptions;

namespace XT.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the XT Spot websocket Api
    /// </summary>
    internal partial class XTSocketClientSpotApi : SocketApiClient, IXTSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _eventPath = MessagePath.Get().Property("event");

        protected override ErrorMapping ErrorMapping => XTErrors.SpotSocketErrors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientSpotApi(ILogger logger, XTSocketOptions options) :
            base(logger, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
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
            => new XTSpotAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTTradeUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTTradeUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data!.Timestamp)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var subscription = new XTSubscription<XTTradeUpdate>(_logger,
                this,
                "trade",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTKlineUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTKlineUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new XTSubscription<XTKlineUpdate>(_logger,
                this,
                "kline",
                symbols.Select(x => x.ToLowerInvariant() + "," + intervalStr).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTOrderBookUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTOrderBookUpdate>(_logger,
                this,
                "depth",
                symbols.Select(x => x.ToLowerInvariant() + "," + depth).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTIncrementalOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTIncrementalOrderBookUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var subscription = new XTSubscription<XTIncrementalOrderBookUpdate>(_logger,
                this,
                "depth_update",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XT24HTicker>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XT24HTicker>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp)
                        .WithSymbol(data.Data.Symbol)
                    );
            });

            var subscription = new XTSubscription<XT24HTicker>(_logger,
                this,
                "ticker",
                symbols.Select(x => x.ToLowerInvariant()).ToArray(),
                internalHandler,
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(string token, Action<DataEvent<XTBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTBalanceUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTBalanceUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTBalanceUpdate>(_logger,
                this,
                "balance",
                null,
                internalHandler,
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string token, Action<DataEvent<XTOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTOrderUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTOrderUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTOrderUpdate>(_logger,
                this,
                "order",
                null,
                internalHandler,
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string token, Action<DataEvent<XTUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTUserTradeUpdate>(data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp)
                    );
            });

            var subscription = new XTSubscription<XTUserTradeUpdate>(_logger,
                this,
                "trade",
                null,
                internalHandler,
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsValid)
                return "pong";

            var id = message.GetValue<string?>(_idPath);
            if (id != null)
                return id;

            return message.GetValue<string>(_eventPath);
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public IXTSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
