using System;
using System.Collections;
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
using XT.Net.Interfaces.Clients.SpotApi;
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
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientSpotApi(ILogger logger, XTSocketOptions options) :
            base(logger, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            RegisterPeriodicQuery(
                "Ping",
                TimeSpan.FromSeconds(20),
                x => new XTPingQuery(),
                (connection, result) =>
                {
                    if (result.Error?.Message.Equals("Query timeout") == true)
                    {
                        // Ping timeout, reconnect
                        _logger.LogWarning("[Sckt {SocketId}] Ping response timeout, reconnecting", connection.SocketId);
                        _ = connection.TriggerReconnectAsync();
                    }
                });
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor(SerializerOptions.WithConverters(XTExchange.SerializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange.SerializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTSpotAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTTradeUpdate>(_logger,
                symbols.Select(x => "trade@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)
                .WithDataTimestamp(x.Data.Timestamp)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var intervalStr = EnumConverter.GetString(interval);
            var subscription = new XTSubscription<XTKlineUpdate>(_logger,
                symbols.Select(x => "kline@" + x.ToLower() + "," + intervalStr).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTOrderBookUpdate>(_logger,
                symbols.Select(x => "depth@" + x.ToLower() + "," + depth).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)
                .WithDataTimestamp(x.Data.Timestamp)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTIncrementalOrderBookUpdate>(_logger,
                symbols.Select(x => "depth_update@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XT24HTicker>(_logger,
                symbols.Select(x => "ticker@" + x.ToLower()).ToArray(),
                x => onMessage(x.WithSymbol(x.Data.Symbol)
                .WithDataTimestamp(x.Data.Timestamp)),
                false);
            return await SubscribeAsync(BaseAddress.AppendPath("public"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(string token, Action<DataEvent<XTBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTBalanceUpdate>(_logger,
                ["balance"],
                x => onMessage(x.WithDataTimestamp(x.Data.Timestamp)),
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string token, Action<DataEvent<XTOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTOrderUpdate>(_logger,
                ["order"],
                x => onMessage(x.WithDataTimestamp(x.Data.Timestamp)),
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }


        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string token, Action<DataEvent<XTUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTUserTradeUpdate>(_logger,
                ["trade"],
                x => onMessage(x.WithDataTimestamp(x.Data.Timestamp)),
                false,
                token);
            return await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
        }
        
        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            if (!message.IsJson)
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
