using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using XT.Net.Objects.Models;
using XT.Net.Enums;
using System.Collections.Generic;
using System;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IXTRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderorderPost" /><br />
        /// Endpoint:<br />
        /// POST /v4/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="orderSide">["<c>side</c>"] Order side</param>
        /// <param name="orderType">["<c>type</c>"] Order type</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="businessType">["<c>bizType</c>"] Business type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity, not supported for market buy orders</param>
        /// <param name="quoteQuantity">["<c>quoteQty</c>"] Quantity in quote asset, required for market buy orders</param>
        /// <param name="price">["<c>price</c>"] Price</param>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrderId>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, TimeInForce timeInForce, BusinessType businessType, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderorderGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/order/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get order info by client order id
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderorderGetQueryParam" /><br />
        /// Endpoint:<br />
        /// GET /v4/order
        /// </para>
        /// </summary>
        /// <param name="clientOrderId">["<c>clientOrderId</c>"] Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderorderDel" /><br />
        /// Endpoint:<br />
        /// DELETE /v4/order/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTCancelId>> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderopenOrderGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/open-order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol</param>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="orderSide">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder[]>> GetOpenOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderhistoryOrderGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/history-order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="orderSide">["<c>side</c>"] Filter by order side</param>
        /// <param name="orderType">["<c>type</c>"] Filter by order type</param>
        /// <param name="orderStatus">["<c>state</c>"] Filter by order status</param>
        /// <param name="hideCanceled">["<c>hiddenCanceled</c>"] Whether hide (true) or return(false) canceled orders</param>
        /// <param name="fromId">["<c>fromId</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTOrder>>> GetClosedOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, OrderStatus? orderStatus = null, bool? hideCanceled = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderopenOrderDel" /><br />
        /// Endpoint:<br />
        /// DELETE /v4/open-order
        /// </para>
        /// </summary>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="orderSide">["<c>side</c>"] Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(BusinessType businessType, string? symbol = null, OrderSide? orderSide = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an active order
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderorderUpdate" /><br />
        /// Endpoint:<br />
        /// PUT /v4/order/{orderId}
        /// </para>
        /// </summary>
        /// <param name="orderId">Id of order to edit</param>
        /// <param name="price">["<c>price</c>"] New price</param>
        /// <param name="quantity">["<c>quantity</c>"] New quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTEditId>> EditOrderAsync(long orderId, decimal quantity, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Get multiple orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#orderbatchOrderGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/batch-order
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>orderIds</c>"] Ids of the orders</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder[]>> GetOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// <para>
        /// Endpoint:<br />
        /// DELETE /v4/batch-order
        /// </para>
        /// </summary>
        /// <param name="orderIds">["<c>orderIds</c>"] Ids of orders to cancel</param>
        /// <param name="clientBatchId">["<c>clientBatchId</c>"] Client batch id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrdersAsync(IEnumerable<long> orderIds, string? clientBatchId = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#tradetradeGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/trade
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="businessType">["<c>bizType</c>"] Filter by business type</param>
        /// <param name="orderSide">["<c>orderSide</c>"] Filter by order side</param>
        /// <param name="orderType">["<c>orderType</c>"] Filter by order type</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="fromId">["<c>fromId</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTUserTrade>>> GetUserTradesAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, long? orderId = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
