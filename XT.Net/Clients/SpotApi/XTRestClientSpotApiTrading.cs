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
using System.Linq;
using CryptoExchange.Net;

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
        public async Task<HttpResult<XTOrderId>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, TimeInForce timeInForce, BusinessType businessType, decimal? quantity = null, decimal? quoteQuantity = null, decimal? price = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("bizType", businessType);
            parameters.Add("quantity", quantity);
#warning these were added as string, check if number is valid
            parameters.Add("quoteQty", quoteQuantity);
            parameters.Add("price", price);
            parameters.Add("clientOrderId", clientOrderId);
            //parameters.Add("media", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange));
            parameters.Add("media", _baseClient.ClientOptions.BrokerId ?? "9231");
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v4/order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTOrderId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<XTOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<XTOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("clientOrderId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, $"/v4/order", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<XTCancelId>> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTCancelId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTOrder[]>> GetOpenOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("bizType", businessType);
            parameters.Add("side", orderSide);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/open-order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTOrder>>> GetClosedOrdersAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, OrderStatus? orderStatus = null, bool? hideCanceled = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("bizType", businessType);
            parameters.Add("side", orderSide);
            parameters.Add("type", orderType);
            parameters.Add("state", orderStatus);
            parameters.Add("hiddenCanceled", hideCanceled);
            parameters.Add("fromId", fromId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/history-order", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(BusinessType businessType, string? symbol = null, OrderSide? orderSide = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("bizType", businessType);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("side", orderSide);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/v4/open-order", XTExchange.RateLimiter.XT, 1, true, parameterPosition: HttpMethodParameterPosition.InBody, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrdersAsync(IEnumerable<long> orderIds, string? clientBatchId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("orderIds", orderIds.ToArray());
            parameters.Add("clientBatchId", clientBatchId);
            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "/v4/batch-order", XTExchange.RateLimiter.XT, 1, true, parameterPosition: HttpMethodParameterPosition.InBody);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<XTEditId>> EditOrderAsync(long orderId, decimal quantity, decimal price, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("price", price);
            parameters.Add("quantity", quantity);
            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, $"/v4/order/{orderId}", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(50, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTEditId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTOrder[]>> GetOrdersAsync(IEnumerable<long> orderIds, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("orderIds", string.Join(",", orderIds));
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/batch-order", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTUserTrade>>> GetUserTradesAsync(string? symbol = null, BusinessType? businessType = null, OrderSide? orderSide = null, OrderType? orderType = null, long? orderId = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("bizType", businessType);
            parameters.Add("orderSide", orderSide);
            parameters.Add("orderType", orderType);
            parameters.Add("orderId", orderId);
            parameters.Add("fromId", fromId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/trade", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTPage<XTUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
