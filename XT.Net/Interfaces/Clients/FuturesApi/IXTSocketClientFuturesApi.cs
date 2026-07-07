using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using XT.Net.Objects.Models;
using System.Collections.Generic;
using XT.Net.Enums;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures streams
    /// </summary>
    public interface IXTSocketClientFuturesApi : ISocketApiClient<XTCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to public trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/TradeRecord" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market trade
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to public trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/TradeRecord" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market trade
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTrade>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/Kline" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market kline
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/Kline" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market kline
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTFuturesKline>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/AggTicker" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/AggTicker" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market ticker
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFuturesTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/AggTicker" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market agg_ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(string symbol, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/AggTicker" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market agg_ticker
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTMarketInfo>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/IndexPrice" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market index_price
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to index price updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/IndexPrice" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market index_price
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/MarkPrice" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market mark_price
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/MarkPrice" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market mark_price
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTPrice>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/IncrementalDepth" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market depth_update
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/IncrementalDepth" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market depth_update
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<XTFuturesIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/LimitedDepth" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market depth
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="depth">Order book levels, 5, 10, 20 or 50</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/LimitedDepth" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market depth
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="depth">Order book levels, 5, 10, 20 or 50</param>
        /// <param name="updateInterval">The update push interval, 100, 250, 500 or 1000ms</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, int? updateInterval, Action<DataEvent<XTFuturesOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/FundRate" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market mark_price
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(string symbol, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to funding rate updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/WebsocKetV2/FundRate" /><br />
        /// Endpoint:<br />
        /// SUB /ws/market mark_price
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToFundingRateUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTFundingRateUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/General_WSS_information" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user balance
        /// </para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates. The listen key is requested internally using the configured API
        /// credentials, cached, and automatically refreshed before each resubscribe (e.g. after a socket reconnect).
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/BalanceChange" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user balance
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToBalancesUpdatesAsync(Action<DataEvent<XTFuturesBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/ChangePosition" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user position
        /// </para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user position updates. The listen key is requested internally using the configured API
        /// credentials, cached, and automatically refreshed before each resubscribe (e.g. after a socket reconnect).
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/ChangePosition" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user position
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<DataEvent<XTFuturesPositionUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/UserOrder" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user order
        /// </para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates. The listen key is requested internally using the configured API
        /// credentials, cached, and automatically refreshed before each resubscribe (e.g. after a socket reconnect).
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/UserOrder" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user order
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(Action<DataEvent<XTFuturesOrder>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/Transactions" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user trade
        /// </para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string listenKey, Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade updates. The listen key is requested internally using the configured API
        /// credentials, cached, and automatically refreshed before each resubscribe (e.g. after a socket reconnect).
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/Transactions" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user trade
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(Action<DataEvent<XTFuturesUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user notifications
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/Message" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user notify
        /// </para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by <see cref="IXTRestClientFuturesApiAccount.GetListenKeyAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(string listenKey, Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user notifications. The listen key is requested internally using the configured API
        /// credentials, cached, and automatically refreshed before each resubscribe (e.g. after a socket reconnect).
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/UserWebsocket/Message" /><br />
        /// Endpoint:<br />
        /// SUB /ws/user notify
        /// </para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<WebSocketResult<UpdateSubscription>> SubscribeToNotificationUpdatesAsync(Action<DataEvent<XTNotification>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IXTSocketClientFuturesApiShared SharedClient { get; }
    }
}
