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
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.TokenManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
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
    internal partial class XTSocketClientSpotApi : SocketApiClient<XTEnvironment, XTSpotAuthenticationProvider, XTCredentials>, IXTSocketClientSpotApi
    {
        #region fields
        protected override ErrorMapping ErrorMapping => XTErrors.SpotSocketErrors;
        private readonly ILoggerFactory? _loggerFactory;
        private XTRestClient? _tokenClient;
        internal TokenManager TokenManager { get; }
        private XTRestClient TokenClient
        {
            get
            {
                if (_tokenClient == null)
                {
                    _tokenClient = new XTRestClient(null, _loggerFactory, Options.Create(new XTRestOptions
                    {
                        ApiCredentials = ApiCredentials,
                        Environment = ClientOptions.Environment,
                        Proxy = ClientOptions.Proxy,
                        OutputOriginalData = ClientOptions.OutputOriginalData
                    }));
                }

                return _tokenClient;
            }
        }
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientSpotApi(ILoggerFactory? loggerFactory, XTSocketOptions options) :
            base(loggerFactory, XTExchange.Metadata.Id, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            _loggerFactory = loggerFactory;

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

            TokenManager = new TokenManager(
                XTExchange.Metadata.Id,
                loggerFactory,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                startToken: StartListenKeyAsync);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType) => new XTSocketMessageHandler();

        /// <inheritdoc />
        protected override XTSpotAuthenticationProvider CreateAuthenticationProvider(XTCredentials credentials)
            => new XTSpotAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTTradeUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithDataTimestamp(data.Data!.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTKlineUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTKlineUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTOrderBookUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTIncrementalOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTIncrementalOrderBookUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.Data.LastUpdateId)
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XT24HTicker>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XT24HTicker>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(Action<DataEvent<XTBalanceUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBalanceUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(string? token, Action<DataEvent<XTBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            if (token == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (token == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTBalanceUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTBalanceUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new XTSubscription<XTBalanceUpdate>(_logger,
                this,
                "balance",
                null,
                internalHandler,
                false,
                token)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<XTOrderUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? token, Action<DataEvent<XTOrderUpdate>> onMessage, CancellationToken ct = default)
        {
            if (token == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (token == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTOrderUpdate>>((receiveTime, originalData, data) =>
            {
                if (data.Data.Timestamp != null)
                    UpdateTimeOffset(data.Data.Timestamp.Value);

                onMessage(
                    new DataEvent<XTOrderUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new XTSubscription<XTOrderUpdate>(_logger,
                this,
                "order",
                null,
                internalHandler,
                false,
                token)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<XTUserTradeUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToUserTradeUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? token, Action<DataEvent<XTUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            if (token == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (token == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTUserTradeUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new XTSubscription<XTUserTradeUpdate>(_logger,
                this,
                "trade",
                null,
                internalHandler,
                false,
                token)
            {
                TokenLease = lease
            };
            var result = await SubscribeAsync(BaseAddress.AppendPath("private"), subscription, ct).ConfigureAwait(false);
            if (!result.Success && lease != null)
                await lease.ReleaseAsync().ConfigureAwait(false);

            return result;
        }

        /// <inheritdoc />
        public IXTSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        protected override async Task<CallResult> RevitalizeRequestAsync(Subscription subscription)
        {
            if (subscription.TokenLease == null)
                return CallResult.Ok(); // Not an authenticated subscription, no need to revitalize

            var scope = new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.Key);

            return await TokenManager.AcquireAndReplaceAsync(subscription, scope).ConfigureAwait(false);
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.SpotApi.Account.GetWebsocketTokenAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }
    }
}
