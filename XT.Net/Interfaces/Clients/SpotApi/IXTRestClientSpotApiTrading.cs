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
        /// <para><a href="https://doc.xt.com/#orderorderPost" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="orderSide">Order side</param>
        /// <param name="orderType">Order type</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="businessType">Business type</param>
        /// <param name="quantity">Quantity, not supported for market buy orders</param>
        /// <param name="quoteQuantity">Quantity in quote asset, required for market buy orders</param>
        /// <param name="price">Price</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrderId>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, TimeInForce timeInForce, BusinessType businessType, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default);

        /// <summary>
        /// Get order info
        /// <para><a href="https://doc.xt.com/#orderorderGet" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get order info by client order id
        /// <para><a href="https://doc.xt.com/#orderorderGetQueryParam" /></para>
        /// </summary>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para><a href="https://doc.xt.com/#orderorderDel" /></para>
        /// </summary>
        /// <param name="orderId">Id of the order</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTCancelId>> CancelOrderAsync(long orderId, CancellationToken ct = default);

        /// <summary>
        /// Get open orders
        /// <para><a href="https://doc.xt.com/#orderopenOrderGet" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol</param>
        /// <param name="businessType">Filter by business type</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder[]>> GetOpenOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, CancellationToken ct = default);

        /// <summary>
        /// Get closed orders
        /// <para><a href="https://doc.xt.com/#orderhistoryOrderGet" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="businessType">Filter by business type</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="orderStatus">Filter by order status</param>
        /// <param name="hideCanceled">Whether hide (true) or return(false) canceled orders</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTOrder>>> GetClosedOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, OrderStatus? orderStatus = null, bool? hideCanceled = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all orders matching the parameters
        /// <para><a href="https://doc.xt.com/#orderopenOrderDel" /></para>
        /// </summary>
        /// <param name="businessType">Filter by business type</param>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(BusinessType businessType, string? symbol = null, OrderSide? orderSide = null, CancellationToken ct = default);

        /// <summary>
        /// Edit an active order
        /// <para><a href="https://doc.xt.com/#orderorderUpdate" /></para>
        /// </summary>
        /// <param name="orderId">Id of order to edit</param>
        /// <param name="price">New price</param>
        /// <param name="quantity">New quantity</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTEditId>> EditOrderAsync(long orderId, decimal quantity, decimal price, CancellationToken ct = default);

        /// <summary>
        /// Get multiple orders
        /// <para><a href="https://doc.xt.com/#orderbatchOrderGet" /></para>
        /// </summary>
        /// <param name="orderIds">Ids of the orders</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrder[]>> GetOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders
        /// </summary>
        /// <param name="orderIds">Ids of orders to cancel</param>
        /// <param name="clientBatchId">Client batch id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelOrdersAsync(IEnumerable<long> orderIds, string? clientBatchId = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para><a href="https://doc.xt.com/#tradetradeGet" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="businessType">Filter by business type</param>
        /// <param name="orderSide">Filter by order side</param>
        /// <param name="orderType">Filter by order type</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTUserTrade>>> GetUserTradesAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, long? orderId = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

    }
}
