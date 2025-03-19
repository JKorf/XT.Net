using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Enums;
using XT.Net.Objects.Models;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Collections.Generic;

namespace XT.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class XTRestClientSpotApiTrading : IXTRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly XTRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal XTRestClientSpotApiTrading(ILogger logger, XTRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrderId>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, TimeInForce timeInForce, BusinessType businessType, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("side", orderSide);
            parameters.AddEnum("type", orderType);
            parameters.AddEnum("timeInForce", timeInForce);
            parameters.AddEnum("bizType", businessType);
            parameters.AddOptionalString("quantity", quantity);
            parameters.AddOptionalString("quoteQty", quoteQuantity);
            parameters.AddOptionalString("price", price);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.Add("media", XTExchange._clientRef);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/v4/order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, $"/v4/order", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTCancelId>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Delete, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTCancelId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrder[]>> GetOpenOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("bizType", businessType);
            parameters.AddOptionalEnum("side", orderSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/open-order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTOrder>>> GetClosedOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, OrderStatus? orderStatus = null, bool? hideCanceled = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("bizType", businessType);
            parameters.AddOptionalEnum("side", orderSide);
            parameters.AddOptionalEnum("type", orderType);
            parameters.AddOptionalEnum("state", orderStatus);
            parameters.AddOptional("hiddenCanceled", hideCanceled);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/history-order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(BusinessType businessType, string? symbol = null, OrderSide? orderSide = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddEnum("bizType", businessType);
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("side", orderSide);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/v4/open-order", XTExchange.RateLimiter.XT, 1, true, parameterPosition: HttpMethodParameterPosition.InBody, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrdersAsync(IEnumerable<long> orderIds, string? clientBatchId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderIds", orderIds);
            parameters.AddOptional("clientBatchId", clientBatchId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, "/v4/batch-order", XTExchange.RateLimiter.XT, 1, true, parameterPosition: HttpMethodParameterPosition.InBody);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTEditId>> EditOrderAsync(long orderId, decimal quantity, decimal price, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("price", price);
            parameters.Add("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Put, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTEditId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrder[]>> GetOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderIds", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/batch-order", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTUserTrade>>> GetUserTradesAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, long? orderId = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("bizType", businessType);
            parameters.AddOptionalEnum("orderSide", orderSide);
            parameters.AddOptionalEnum("orderType", orderType);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/trade", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTPage<XTUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
