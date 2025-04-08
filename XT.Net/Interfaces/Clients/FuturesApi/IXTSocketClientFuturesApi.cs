using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using XT.Net.Objects.Models;
using System.Collections.Generic;
using XT.Net.Enums;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures streams
    /// </summary>
    public interface IXTSocketClientFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2dealRecord" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2dealRecord" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2symbolKline" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2symbolKline" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2tickerRealTime" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2tickerRealTime" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2aggTickerRealTime" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2aggTickerRealTime" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2indexPrice" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2indexPrice" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2markPrice" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2markPrice" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2increDepth" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2increDepth" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2limitDepth" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="depth">Order book levels, 5, 10, 20 or 50</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2limitDepth" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="depth">Order book levels, 5, 10, 20 or 50</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2fundRate" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para><a href="https://doc.xt.com/#futures_market_websocket_v2fundRate" /></para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para><a href="https://doc.xt.com/#futures_user_websocket_v2base" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates
        /// <para><a href="https://doc.xt.com/#futures_user_websocket_v2position" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para><a href="https://doc.xt.com/#futures_user_websocket_v2order" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para><a href="https://doc.xt.com/#futures_user_websocket_v2trade" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user notifications
        /// <para><a href="https://doc.xt.com/#futures_user_websocket_v2notify" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(string listenKey, Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IXTSocketClientFuturesApiShared SharedClient { get; }
    }
}
