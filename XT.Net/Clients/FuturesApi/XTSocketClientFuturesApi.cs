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
    internal partial class XTSocketClientFuturesApi : SocketApiClient<XTEnvironment, XTFuturesAuthenticationProvider, XTCredentials>, IXTSocketClientFuturesApi
    {
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
        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientFuturesApi(ILoggerFactory? loggerFactory, XTSocketOptions options) :
            base(loggerFactory, XTExchange.Metadata.Id, options.Environment.FuturesSocketClientAddress!, options, options.FuturesOptions)
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
        protected override XTFuturesAuthenticationProvider CreateAuthenticationProvider(XTCredentials credentials)
            => new XTFuturesAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
            => await SubscribeToTradeUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesTrade>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFuturesTrade>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync([symbol], interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesKline>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesKline>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesTicker>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFuturesTicker>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
            => await SubscribeToAggregatedTickerUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTMarketInfo>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTMarketInfo>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToIndexPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTPrice>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTPrice>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
            => await SubscribeToMarkPriceUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTPrice>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTPrice>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToIncrementalOrderBookUpdatesAsync([symbol], updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesIncrementalOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFuturesIncrementalOrderBookUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                        .WithSequenceNumber(data.Data.LastUpdateId)
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync([symbol], depth, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFuturesOrderBookUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToFundingRateUpdatesAsync([symbol], onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default)
        {
            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFundingRateUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFundingRateUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
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
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToBalancesUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(string? listenKey, Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesBalanceUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesBalanceUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesBalanceUpdate>(_logger,
                this,
                "balance",
                listenKey,
                internalHandler)
            {
                TokenLease = lease
            };
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToPositionUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string? listenKey, Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesPositionUpdate>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesPositionUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesPositionUpdate>(_logger,
                this,
                "position",
                listenKey,
                internalHandler)
            {
                TokenLease = lease
            };
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default)
            => SubscribeToOrderUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? listenKey, Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesOrder>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTFuturesOrder>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesOrder>(_logger,
                this,
                "order",
                listenKey,
                internalHandler)
            {
                TokenLease = lease
            };
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default)
            => SubscribeToUserTradeUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string? listenKey, Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTFuturesUserTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.Timestamp);

                onMessage(
                    new DataEvent<XTFuturesUserTradeUpdate>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                        .WithDataTimestamp(data.Data.Timestamp, GetTimeOffset())
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTFuturesUserTradeUpdate>(_logger,
                this,
                "trade",
                listenKey,
                internalHandler)
            {
                TokenLease = lease
            };
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default)
            => SubscribeToNotificationUpdatesAsync(null, onMessage, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(string? listenKey, Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default)
        {
            if (listenKey == null && !Authenticated)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    XTExchange.Metadata.Id,
                    EnvironmentName,
                    "Futures",
                    ApiCredentials!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var internalHandler = new Action<DateTime, string?, XTSocketUpdate<XTNotification>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<XTNotification>(XTExchange.ExchangeName, data.Data!, receiveTime, originalData)
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithStreamId(data.Event)
                    );
            });

            var subscription = new XTFuturesAuthSubscription<XTNotification>(_logger,
                this,
                "notify",
                listenKey,
                internalHandler)
            {
                TokenLease = lease
            };
            return await SubscribeAsync(BaseAddress.AppendPath("ws/user"), subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public IXTSocketClientFuturesApiShared SharedClient => this;

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
                    "Futures",
                    ApiCredentials!.Key);

            return await TokenManager.AcquireAndReplaceAsync(subscription, scope).ConfigureAwait(false);
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.UsdtFuturesApi.Account.GetListenKeyAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }
    }
}
