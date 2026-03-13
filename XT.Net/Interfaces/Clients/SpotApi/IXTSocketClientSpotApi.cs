using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects.Sockets;
using XT.Net.Objects.Models;
using XT.Net.Enums;
using System.Collections.Generic;
using CryptoExchange.Net.Interfaces.Clients;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot streams
    /// </summary>
    public interface IXTSocketClientSpotApi : ISocketApiClient<XTCredentials>, IDisposable
    {
        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicdealRecord" /><br />
        /// Endpoint:<br />
        /// SUB /public trade
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to trade updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicdealRecord" /><br />
        /// Endpoint:<br />
        /// SUB /public trade
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicsymbolKline" /><br />
        /// Endpoint:<br />
        /// SUB /public kline
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicsymbolKline" /><br />
        /// Endpoint:<br />
        /// SUB /public kline
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="interval">Interval of the kline</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<XTKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates, pushes the selected depth at each update
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publiclimitDepth" /><br />
        /// Endpoint:<br />
        /// SUB /public depth
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="depth">Book depth, 5, 10, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book updates, pushes the selected depth at each update
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publiclimitDepth" /><br />
        /// Endpoint:<br />
        /// SUB /public depth
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="depth">Book depth, 5, 10, 20 or 50</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int depth, Action<DataEvent<XTOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicincreDepth" /><br />
        /// Endpoint:<br />
        /// SUB /public depth_update
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string symbol, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to incremental order book updates, only pushes changed entries
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publicincreDepth" /><br />
        /// Endpoint:<br />
        /// SUB /public depth_update
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XTIncrementalOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publictickerRealTime" /><br />
        /// Endpoint:<br />
        /// SUB /public ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to ticker updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_publictickerRealTime" /><br />
        /// Endpoint:<br />
        /// SUB /public ticker
        /// </para>
        /// </summary>
        /// <param name="symbols">Symbol names</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<XT24HTicker>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user balance updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_privatebalanceChange" /><br />
        /// Endpoint:<br />
        /// SUB /private balance
        /// </para>
        /// </summary>
        /// <param name="token">Websocket token, can be retrieved using <see cref="IXTRestClientSpotApiAccount.GetWebsocketTokenAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBalanceUpdatesAsync(string token, Action<DataEvent<XTBalanceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user order updates
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_privateorderChange" /><br />
        /// Endpoint:<br />
        /// SUB /private order
        /// </para>
        /// </summary>
        /// <param name="token">Websocket token, can be retrieved using <see cref="IXTRestClientSpotApiAccount.GetWebsocketTokenAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string token, Action<DataEvent<XTOrderUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to user trade update
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#websocket_privateorderDeal" /><br />
        /// Endpoint:<br />
        /// SUB /private trade
        /// </para>
        /// </summary>
        /// <param name="token">Websocket token, can be retrieved using <see cref="IXTRestClientSpotApiAccount.GetWebsocketTokenAsync(CancellationToken)" /></param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserTradeUpdatesAsync(string token, Action<DataEvent<XTUserTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IXTSocketClientSpotApiShared SharedClient { get; }
    }
}
