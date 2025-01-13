using System.Threading.Tasks;
using System.Threading;
using XT.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using System.Collections.Generic;
using System;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IXTRestClientFuturesApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://doc.xt.com/#futures_ordercreate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="price">Limit price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="triggerProfitPrice">Trigger profit price</param>
        /// <param name="triggerStopPrice">Trigger stop price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, PositionSide positionSide, decimal? price = null, TimeInForce? timeInForce = null, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single call
        /// <para><a href="https://doc.xt.com/#futures_ordercreateBatch" /></para>
        /// </summary>
        /// <param name="orders">Order requests</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> PlaceMultipleOrdersAsync(IEnumerable<XTFuturesOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para><a href="https://doc.xt.com/#futures_orderupdate" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="price">Price</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="triggerProfitPrice">Trigger profit price</param>
        /// <param name="triggerStopPrice">Trigger stop price</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="profitOrderType">Take profit order type</param>
        /// <param name="profitTimeInForce">Take profit time in force</param>
        /// <param name="profitPrice">Take profit order price</param>
        /// <param name="lossOrderType">Stop loss order type</param>
        /// <param name="lossTimeInForce">Stop loss time in force</param>
        /// <param name="stopPrice">Stop loss order price</param>
        /// <param name="followUpOrder">If true, it indicates chase order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditOrderAsync(long orderId, decimal price, decimal quantity, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, PriceType? triggerPriceType = null, OrderType? profitOrderType = null, TimeInForce? profitTimeInForce = null, decimal? profitPrice = null, OrderType? lossOrderType = null, TimeInForce? lossTimeInForce = null, string? stopPrice = null, bool? followUpOrder = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para><a href="https://doc.xt.com/#futures_ordergetHistory" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://doc.xt.com/#futures_ordergetTrades" /></para>
        /// </summary>
        /// <param name="symbol">Filter b symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTFuturesUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para><a href="https://doc.xt.com/#futures_ordergetById" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para><a href="https://doc.xt.com/#futures_ordergetOrders" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        
        /// <summary>
        /// Cancel an order
        /// <para><a href="https://doc.xt.com/#futures_ordercancel" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para><a href="https://doc.xt.com/#futures_ordercancel" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get open positions
        /// <para><a href="https://doc.xt.com/#futures_usergetActivitePosition" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTPosition>>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position info
        /// <para><a href="https://doc.xt.com/#futures_usergetPosition" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTPositionInfo>>> GetPositionsInfoAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Close all open positions
        /// <para><a href="https://doc.xt.com/#futures_usercloseAllPosition" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CloseAllPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin call info
        /// <para><a href="https://doc.xt.com/#futures_usergetMarginCall" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTMarginCallInfo>>> GetMarginCallInfoAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para><a href="https://doc.xt.com/#futures_entrustcreatePlan" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="tpSlOrderType">Order type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="orderPrice">Order price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceTriggerOrderAsync(string symbol, OrderSide orderSide, TriggerOrderType tpSlOrderType, decimal quantity, decimal stopPrice, TimeInForce timeInForce, PriceType triggerPriceType, PositionSide positionSide, decimal? orderPrice = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelPlan" /></para>
        /// </summary>
        /// <param name="triggerOrderId">Trigger order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTriggerOrderAsync(long triggerOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all trigger orders for a symbol
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelPlanBatch" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllTriggerOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para><a href="https://doc.xt.com/#futures_entrustgetPlan" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get trigger order
        /// <para><a href="https://doc.xt.com/#futures_entrustgetPlanById" /></para>
        /// </summary>
        /// <param name="orderId">Trigger order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTriggerOrder>> GetTriggerOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get closed trigger orders
        /// <para><a href="https://doc.xt.com/#futures_entrustgetPlanHistory" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">Filter by id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTriggerOrder>>> GetClosedTriggerOrdersAsync(string? symbol = null, long? orderId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new stop limit order
        /// <para><a href="https://doc.xt.com/#futures_entrustcreateProfit" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="triggerProfitPrice">Trigger profit price</param>
        /// <param name="triggerStopPrice">Trigger stop price</param>
        /// <param name="expireTime">Expire time</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceStopLimitOrderAsync(string symbol, decimal quantity, decimal triggerProfitPrice, decimal triggerStopPrice, DateTime expireTime, PositionSide positionSide, CancellationToken ct = default);

        /// <summary>
        /// Cancel a stop limit order
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelProfit" /></para>
        /// </summary>
        /// <param name="orderId">Stop limit order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelStopLimitOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all stop limit orders for a symbol
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelProfitBatch" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllStopLimitOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para><a href="https://doc.xt.com/#futures_entrustgetProfit" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">Filter by status</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTStopLimitOrder>>> GetStopLimitOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get stop limit order
        /// <para><a href="https://doc.xt.com/#futures_entrustgetProfitById" /></para>
        /// </summary>
        /// <param name="orderId">Stop limit order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTStopLimitOrder>> GetStopLimitOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Edit a stop limit order
        /// <para><a href="https://doc.xt.com/#futures_entrustupdateProfit" /></para>
        /// </summary>
        /// <param name="orderId">Order id</param>
        /// <param name="triggerProfitPrice">Trigger profit price</param>
        /// <param name="triggerStopPrice">Trigger stop price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditStopLimitOrderAsync(long orderId, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para><a href="https://doc.xt.com/#futures_entrustcreateTrack" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="positionSide ">Position side</param>
        /// <param name="positionType">Position type</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="trackRange">Callback range config</param>
        /// <param name="callbackValue">Callback value</param>
        /// <param name="triggerPriceType">Trigger price type</param>
        /// <param name="activationPrice">Activation price</param>
        /// <param name="clientMedia">Client media</param>
        /// <param name="clientMediaChannel">Client media channel</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="expireTime">Expire time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceTrackOrderAsync(string symbol, OrderSide orderSide, PositionSide positionSide, PositionType positionType, decimal quantity, TrackRange trackRange, decimal callbackValue, PriceType triggerPriceType, decimal? activationPrice = null, string? clientMedia = null, string? clientMediaChannel = null, string? clientOrderId = null, DateTime? expireTime = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a track order
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelTrack" /></para>
        /// </summary>
        /// <param name="orderId">Track order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTrackOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get trigger order
        /// <para><a href="https://doc.xt.com/#futures_entrustgetTrackDetail" /></para>
        /// </summary>
        /// <param name="orderId">Track order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTrackOrder>> GetTrackOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get open track orders
        /// <para><a href="https://doc.xt.com/#futures_entrustgetTrackList" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTTrackOrder>>> GetOpenTrackOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all track orders for a symbol
        /// <para><a href="https://doc.xt.com/#futures_entrustcancelAllTrack" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllTrackOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get closed track orders
        /// <para><a href="https://doc.xt.com/#futures_entrustgetTrackHistoryList" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symboll</param>
        /// <param name="status">Filter by order status</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="page">Page</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTrackOrder>>> GetClosedTrackOrdersAsync(string? symbol = null, TrackOrderStatus? status = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
