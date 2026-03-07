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
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordercreate" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/order/create
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderSide">["<c>orderSide</c>"] Order side</param>
        /// <param name="orderType">["<c>orderType</c>"] Order type</param>
        /// <param name="quantity">["<c>origQty</c>"] Order quantity</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="triggerProfitPrice">["<c>triggerProfitPrice</c>"] Trigger profit price</param>
        /// <param name="triggerStopPrice">["<c>triggerStopPrice</c>"] Trigger stop price</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, PositionSide positionSide, decimal? price = null, TimeInForce? timeInForce = null, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordercreateBatch" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v2/order/create-batch
        /// </para>
        /// </summary>
        /// <param name="orders">["<c>list</c>"] Order requests</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> PlaceMultipleOrdersAsync(IEnumerable<XTFuturesOrderRequest> orders, CancellationToken ct = default);

        /// <summary>
        /// Edit an existing order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_orderupdate" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/order/update
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="price">["<c>price</c>"] Price</param>
        /// <param name="quantity">["<c>origQty</c>"] Quantity</param>
        /// <param name="triggerProfitPrice">["<c>triggerProfitPrice</c>"] Trigger profit price</param>
        /// <param name="triggerStopPrice">["<c>triggerStopPrice</c>"] Trigger stop price</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="profitOrderType">["<c>profitDelegateOrderType</c>"] Take profit order type</param>
        /// <param name="profitTimeInForce">["<c>profitDelegateTimeInForce</c>"] Take profit time in force</param>
        /// <param name="profitPrice">["<c>profitDelegatePrice</c>"] Take profit order price</param>
        /// <param name="lossOrderType">["<c>stopDelegateOrderType</c>"] Stop loss order type</param>
        /// <param name="lossTimeInForce">["<c>stopDelegateTimeInForce</c>"] Stop loss time in force</param>
        /// <param name="stopPrice">["<c>stopDelegatePrice</c>"] Stop loss order price</param>
        /// <param name="followUpOrder">["<c>followUpOrder</c>"] If true, it indicates chase order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditOrderAsync(long orderId, decimal price, decimal quantity, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, PriceType? triggerPriceType = null, OrderType? profitOrderType = null, TimeInForce? profitTimeInForce = null, decimal? profitPrice = null, OrderType? lossOrderType = null, TimeInForce? lossTimeInForce = null, string? stopPrice = null, bool? followUpOrder = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordergetHistory" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/order/list-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="fromId">["<c>id</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordergetTrades" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/order/trade-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter b symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTFuturesUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get order by id
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordergetById" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/order/detail
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordergetOrders" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/order/list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>state</c>"] Filter by status</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
        
        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordercancel" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/order/cancel
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>orderId</c>"] Order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_ordercancel" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/order/cancel-all
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetActivitePosition" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/position
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get position info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetPosition" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/position/list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPositionInfo[]>> GetPositionsInfoAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Close all open positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usercloseAllPosition" /><br />
        /// Endpoint:<br />
        /// POST /future/user/v1/position/close-all
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CloseAllPositionsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get margin call info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetMarginCall" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/position/break-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTMarginCallInfo[]>> GetMarginCallInfoAsync(string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcreatePlan" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/create-plan
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderSide">["<c>orderSide</c>"] Order side</param>
        /// <param name="tpSlOrderType">["<c>entrustType</c>"] Order type</param>
        /// <param name="quantity">["<c>origQty</c>"] Quantity</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Stop price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="orderPrice">["<c>price</c>"] Order price</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceTriggerOrderAsync(string symbol, OrderSide orderSide, TriggerOrderType tpSlOrderType, decimal quantity, decimal stopPrice, TimeInForce timeInForce, PriceType triggerPriceType, PositionSide positionSide, decimal? orderPrice = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelPlan" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-plan
        /// </para>
        /// </summary>
        /// <param name="triggerOrderId">["<c>entrustId</c>"] Trigger order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTriggerOrderAsync(long triggerOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all trigger orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelPlanBatch" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-all-plan
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllTriggerOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetPlan" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/plan-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>state</c>"] Filter by status</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetPlanById" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/plan-detail
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>entrustId</c>"] Trigger order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTriggerOrder>> GetTriggerOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get closed trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetPlanHistory" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/plan-list-history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="orderId">["<c>id</c>"] Filter by id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTriggerOrder>>> GetClosedTriggerOrdersAsync(string? symbol = null, long? orderId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Place a new stop limit order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcreateProfit" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/create-profit
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="quantity">["<c>origQty</c>"] Quantity</param>
        /// <param name="triggerProfitPrice">["<c>triggerProfitPrice</c>"] Trigger profit price</param>
        /// <param name="triggerStopPrice">["<c>triggerStopPrice</c>"] Trigger stop price</param>
        /// <param name="expireTime">["<c>expireTime</c>"] Expire time</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceStopLimitOrderAsync(string symbol, decimal quantity, decimal triggerProfitPrice, decimal triggerStopPrice, DateTime expireTime, PositionSide positionSide, CancellationToken ct = default);

        /// <summary>
        /// Cancel a stop limit order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelProfit" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-profit-stop
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>profitId</c>"] Stop limit order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelStopLimitOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel all stop limit orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelProfitBatch" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-all-profit-stop
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllStopLimitOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get trigger orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetProfit" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/profit-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH_USDT`</param>
        /// <param name="status">["<c>state</c>"] Filter by status</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTStopLimitOrder>>> GetStopLimitOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Get stop limit order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetProfitById" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/profit-detail
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>profitId</c>"] Stop limit order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTStopLimitOrder>> GetStopLimitOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Edit a stop limit order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustupdateProfit" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/update-profit-stop
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>profitId</c>"] Order id</param>
        /// <param name="triggerProfitPrice">["<c>triggerProfitPrice</c>"] Trigger profit price</param>
        /// <param name="triggerStopPrice">["<c>triggerStopPrice</c>"] Trigger stop price</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> EditStopLimitOrderAsync(long orderId, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, CancellationToken ct = default);

        /// <summary>
        /// 
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcreateTrack" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/create-track
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="orderSide">["<c>orderSide</c>"] Order side</param>
        /// <param name="positionSide ">["<c>positionSide</c>"] Position side</param>
        /// <param name="positionType">["<c>positionType</c>"] Position type</param>
        /// <param name="quantity">["<c>origQty</c>"] Quantity</param>
        /// <param name="trackRange">["<c>callback</c>"] Callback range config</param>
        /// <param name="callbackValue">["<c>callbackVal</c>"] Callback value</param>
        /// <param name="triggerPriceType">["<c>triggerPriceType</c>"] Trigger price type</param>
        /// <param name="activationPrice">["<c>activationPrice</c>"] Activation price</param>
        /// <param name="clientMedia">["<c>clientMedia</c>"] Client media</param>
        /// <param name="clientMediaChannel">["<c>clientMediaChannel</c>"] Client media channel</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="expireTime">["<c>expireTime</c>"] Expire time</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> PlaceTrackOrderAsync(string symbol, OrderSide orderSide, PositionSide positionSide, PositionType positionType, decimal quantity, TrackRange trackRange, decimal callbackValue, PriceType triggerPriceType, decimal? activationPrice = null, string? clientMedia = null, string? clientMediaChannel = null, string? clientOrderId = null, DateTime? expireTime = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel a track order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelTrack" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-track
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>trackId</c>"] Track order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelTrackOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get trigger order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetTrackDetail" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/track-detail
        /// </para>
        /// </summary>
        /// <param name="orderId">["<c>trackId</c>"] Track order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTrackOrder>> GetTrackOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get open track orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetTrackList" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/track-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>pageSize</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDataPage<XTTrackOrder>>> GetOpenTrackOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all track orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustcancelAllTrack" /><br />
        /// Endpoint:<br />
        /// POST /future/trade/v1/entrust/cancel-all-track
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH_USDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllTrackOrdersAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get closed track orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_entrustgetTrackHistoryList" /><br />
        /// Endpoint:<br />
        /// GET /future/trade/v1/entrust/profit-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symboll</param>
        /// <param name="status">["<c>state</c>"] Filter by order status</param>
        /// <param name="orderId">["<c>id</c>"] Filter by order id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="page">["<c>page</c>"] Page</param>
        /// <param name="pageSize">["<c>size</c>"] Page size</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTrackOrder>>> GetClosedTrackOrdersAsync(string? symbol = null, TrackOrderStatus? status = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default);
    }
}
