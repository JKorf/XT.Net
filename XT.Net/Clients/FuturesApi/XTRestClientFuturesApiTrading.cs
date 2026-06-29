using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;
using XT.Net.Enums;
using CryptoExchange.Net.RateLimiting.Guards;
using System.Collections.Generic;
using System.Linq;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net;

namespace XT.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class XTRestClientFuturesApiTrading : IXTRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly XTRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal XTRestClientFuturesApiTrading(ILogger logger, XTRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<long>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, PositionSide positionSide, decimal? price = null, TimeInForce? timeInForce = null, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("orderSide", orderSide);
            parameters.Add("orderType", orderType);
            parameters.Add("origQty", quantity);
            parameters.Add("positionSide", positionSide);
            parameters.Add("price", price);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("triggerProfitPrice", triggerProfitPrice);
            parameters.Add("triggerStopPrice", triggerStopPrice);
            parameters.Add("clientOrderId", clientOrderId);
            //parameters.Add("media", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange));
            parameters.Add("media", _baseClient.ClientOptions.BrokerId ?? "9231");
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/order/create", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult> PlaceMultipleOrdersAsync(IEnumerable<XTFuturesOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            foreach (var order in orders)
            {
                //order.Media = LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange);
                order.Media = _baseClient.ClientOptions.BrokerId ?? "9231";
            }

            parameters.Add("list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v2/order/create-batch", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult> EditOrderAsync(long orderId, decimal price, decimal quantity, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, PriceType? triggerPriceType = null, OrderType? profitOrderType = null, TimeInForce? profitTimeInForce = null, decimal? profitPrice = null, OrderType? lossOrderType = null, TimeInForce? lossTimeInForce = null, string? stopPrice = null, bool? followUpOrder = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            parameters.Add("price", price);
            parameters.Add("origQty", quantity);
            parameters.Add("triggerProfitPrice", triggerProfitPrice);
            parameters.Add("triggerStopPrice", triggerStopPrice);
            parameters.Add("triggerPriceType", triggerPriceType);
            parameters.Add("profitDelegateOrderType", profitOrderType);
            parameters.Add("profitDelegateTimeInForce", profitTimeInForce);
            parameters.Add("profitDelegatePrice", profitPrice);
            parameters.Add("stopDelegateOrderType", lossOrderType);
            parameters.Add("stopDelegateTimeInForce", lossTimeInForce);
            parameters.Add("stopDelegatePrice", stopPrice);
            parameters.Add("followUpOrder", followUpOrder);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/order/update", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("id", fromId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/order/list-history", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<XTDataPage<XTFuturesUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("orderId", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/order/trade-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTFuturesUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/order/detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result.Success && result.Data == null)
                return HttpResult.Fail<XTFuturesOrder>(result, new ServerError(new ErrorInfo(ErrorType.UnknownOrder, "Order not found")));

            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTDataPage<XTFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("state", status);
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/order/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesOrder[]>> GetOpenOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/order/list-open-order", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesOrder[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/order/cancel", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/order/cancel-all", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<XTPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/position", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync<XTPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions Info

        /// <inheritdoc />
        public async Task<HttpResult<XTPositionInfo[]>> GetPositionsInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/position/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPositionInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close All Positions

        /// <inheritdoc />
        public async Task<HttpResult> CloseAllPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/user/v1/position/close-all", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Margin Call Info

        /// <inheritdoc />
        public async Task<HttpResult<XTMarginCallInfo[]>> GetMarginCallInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/position/break-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTMarginCallInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<long>> PlaceTriggerOrderAsync(string symbol, OrderSide orderSide, TriggerOrderType tpSlOrderType, decimal quantity, decimal stopPrice, TimeInForce timeInForce, PriceType triggerPriceType, PositionSide positionSide, decimal? orderPrice = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("orderSide", orderSide);
            parameters.Add("entrustType", tpSlOrderType);
            parameters.Add("origQty", quantity);
            parameters.Add("stopPrice", stopPrice);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("triggerPriceType", triggerPriceType);
            parameters.Add("positionSide", positionSide);
            parameters.Add("price", orderPrice);
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("media", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/create-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelTriggerOrderAsync(long triggerOrderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("entrustId", triggerOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllTriggerOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-all-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTDataPage<XTTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("state", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/plan-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTTriggerOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<HttpResult<XTTriggerOrder>> GetTriggerOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("entrustId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/plan-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTriggerOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTTriggerOrder>>> GetClosedTriggerOrdersAsync(string? symbol = null, long? orderId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("id", orderId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/plan-list-history", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTTriggerOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Stop Limit Order

        /// <inheritdoc />
        public async Task<HttpResult<long>> PlaceStopLimitOrderAsync(string symbol, decimal quantity, decimal triggerProfitPrice, decimal triggerStopPrice, DateTime expireTime, PositionSide positionSide, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("origQty", quantity);
            parameters.Add("triggerProfitPrice", triggerProfitPrice);
            parameters.Add("triggerStopPrice", triggerStopPrice);
            parameters.Add("expireTime", expireTime);
            parameters.Add("positionSide", positionSide);
            parameters.Add("media", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/create-profit", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Cancel Stop Limit Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelStopLimitOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("profitId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Stop Limit Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllStopLimitOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-all-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTDataPage<XTStopLimitOrder>>> GetStopLimitOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("state", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/profit-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTStopLimitOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Stop Limit Order

        /// <inheritdoc />
        public async Task<HttpResult<XTStopLimitOrder>> GetStopLimitOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("profitId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/profit-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTStopLimitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Stop Limit Order

        /// <inheritdoc />
        public async Task<HttpResult> EditStopLimitOrderAsync(long orderId, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("profitId", orderId);
            parameters.Add("triggerProfitPrice", triggerProfitPrice);
            parameters.Add("triggerStopPrice", triggerStopPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/update-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Track Order

        /// <inheritdoc />
        public async Task<HttpResult<long>> PlaceTrackOrderAsync(string symbol, OrderSide orderSide, PositionSide positionSide, PositionType positionType, decimal quantity, TrackRange trackRange, decimal callbackValue, PriceType triggerPriceType, decimal? activationPrice = null, string? clientMedia = null, string? clientMediaChannel = null, string? clientOrderId = null, DateTime? expireTime = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("orderSide", orderSide);
            parameters.Add("positionSide", positionSide);
            parameters.Add("positionType", positionType);
            parameters.Add("origQty", quantity);
            parameters.Add("callback", trackRange);
            parameters.Add("callbackVal", callbackValue);
            parameters.Add("triggerPriceType", triggerPriceType);
            parameters.Add("activationPrice", activationPrice);
            parameters.Add("clientMedia", clientMedia);
            parameters.Add("clientMediaChannel", clientMediaChannel);
            parameters.Add("clientOrderId", clientOrderId);
            parameters.Add("expireTime", expireTime);
            parameters.Add("media", LibraryHelpers.GetClientReference(() => _baseClient.ClientOptions.BrokerId, _baseClient.Exchange));
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/create-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Cancel Track Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelTrackOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("trackId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Track Order

        /// <inheritdoc />
        public async Task<HttpResult<XTTrackOrder>> GetTrackOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("trackId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/track-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTrackOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Track Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTDataPage<XTTrackOrder>>> GetOpenTrackOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/track-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTTrackOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Track Orders

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllTrackOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/trade/v1/entrust/cancel-all-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Track Orders

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTTrackOrder>>> GetClosedTrackOrdersAsync(string? symbol = null, TrackOrderStatus? status = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("id", orderId);
            parameters.Add("state", status);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("page", page);
            parameters.Add("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/trade/v1/entrust/profit-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTTrackOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
