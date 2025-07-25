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
        public async Task<WebCallResult<long>> PlaceOrderAsync(string symbol, OrderSide orderSide, OrderType orderType, decimal quantity, PositionSide positionSide, decimal? price = null, TimeInForce? timeInForce = null, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddEnum("orderSide", orderSide);
            parameters.AddEnum("orderType", orderType);
            parameters.Add("origQty", quantity);
            parameters.AddEnum("positionSide", positionSide);
            parameters.AddOptional("price", price);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("triggerProfitPrice", triggerProfitPrice);
            parameters.AddOptional("triggerStopPrice", triggerStopPrice);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.Add("media", XTExchange._clientRef);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/order/create", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            return result.As<long>(result.Data ?? 0);
        }

        #endregion

        #region Place Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult> PlaceMultipleOrdersAsync(IEnumerable<XTFuturesOrderRequest> orders, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("list", orders.ToArray());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/order/create-batch", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditOrderAsync(long orderId, decimal price, decimal quantity, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, PriceType? triggerPriceType = null, OrderType? profitOrderType = null, TimeInForce? profitTimeInForce = null, decimal? profitPrice = null, OrderType? lossOrderType = null, TimeInForce? lossTimeInForce = null, string? stopPrice = null, bool? followUpOrder = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            parameters.Add("price", price);
            parameters.Add("origQty", quantity);
            parameters.AddOptional("triggerProfitPrice", triggerProfitPrice);
            parameters.AddOptional("triggerStopPrice", triggerStopPrice);
            parameters.AddOptionalEnum("triggerPriceType", triggerPriceType);
            parameters.AddOptionalEnum("profitDelegateOrderType", profitOrderType);
            parameters.AddOptionalEnum("profitDelegateTimeInForce", profitTimeInForce);
            parameters.AddOptional("profitDelegatePrice", profitPrice);
            parameters.AddOptionalEnum("stopDelegateOrderType", lossOrderType);
            parameters.AddOptionalEnum("stopDelegateTimeInForce", lossTimeInForce);
            parameters.AddOptional("stopDelegatePrice", stopPrice);
            parameters.AddOptional("followUpOrder", followUpOrder);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/order/update", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTFuturesOrder>>> GetClosedOrdersAsync(string? symbol = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("id", fromId);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/order/list-history", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<XTDataPage<XTFuturesUserTrade>>> GetUserTradesAsync(string? symbol = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/order/trade-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTFuturesUserTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/order/detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesOrder>(request, parameters, ct).ConfigureAwait(false);
            if (result && result.Data == null)
                return result.AsError<XTFuturesOrder>(new ServerError("Order not found"));

            return result;
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTDataPage<XTFuturesOrder>>> GetOrdersAsync(string? symbol = null, OrderStatus? status = null, string? clientOrderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptionalEnum("state", status);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/order/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTFuturesOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("orderId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/order/cancel", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/order/cancel-all", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<XTPosition[]>> GetPositionsAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/user/v1/position", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync<XTPosition[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Positions Info

        /// <inheritdoc />
        public async Task<WebCallResult<XTPositionInfo[]>> GetPositionsInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/user/v1/position/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPositionInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Close All Positions

        /// <inheritdoc />
        public async Task<WebCallResult> CloseAllPositionsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/user/v1/position/close-all", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Margin Call Info

        /// <inheritdoc />
        public async Task<WebCallResult<XTMarginCallInfo[]>> GetMarginCallInfoAsync(string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/user/v1/position/break-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTMarginCallInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceTriggerOrderAsync(string symbol, OrderSide orderSide, TriggerOrderType tpSlOrderType, decimal quantity, decimal stopPrice, TimeInForce timeInForce, PriceType triggerPriceType, PositionSide positionSide, decimal? orderPrice = null, string? clientOrderId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddEnum("orderSide", orderSide);
            parameters.AddEnum("entrustType", tpSlOrderType);
            parameters.Add("origQty", quantity);
            parameters.Add("stopPrice", stopPrice);
            parameters.AddEnum("timeInForce", timeInForce);
            parameters.AddEnum("triggerPriceType", triggerPriceType);
            parameters.AddEnum("positionSide", positionSide);
            parameters.AddOptional("price", orderPrice);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.Add("media", XTExchange._clientRef);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/create-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? 0);
        }

        #endregion

        #region Cancel Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTriggerOrderAsync(long triggerOrderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("entrustId", triggerOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllTriggerOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-all-plan", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTDataPage<XTTriggerOrder>>> GetTriggerOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptionalEnum("state", status);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/plan-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTTriggerOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTTriggerOrder>> GetTriggerOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("entrustId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/plan-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTriggerOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTTriggerOrder>>> GetClosedTriggerOrdersAsync(string? symbol = null, long? orderId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("id", orderId);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/plan-list-history", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTTriggerOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Stop Limit Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceStopLimitOrderAsync(string symbol, decimal quantity, decimal triggerProfitPrice, decimal triggerStopPrice, DateTime expireTime, PositionSide positionSide, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("origQty", quantity);
            parameters.Add("triggerProfitPrice", triggerProfitPrice);
            parameters.Add("triggerStopPrice", triggerStopPrice);
            parameters.AddMilliseconds("expireTime", expireTime);
            parameters.AddEnum("positionSide", positionSide);
            parameters.Add("media", XTExchange._clientRef);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/create-profit", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            return result.As(result.Data ?? 0);
        }

        #endregion

        #region Cancel Stop Limit Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelStopLimitOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("profitId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Stop Limit Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllStopLimitOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-all-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trigger Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTDataPage<XTStopLimitOrder>>> GetStopLimitOrdersAsync(string? symbol = null, TriggerOrderStatus? status = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptionalEnum("state", status);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/profit-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTStopLimitOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Stop Limit Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTStopLimitOrder>> GetStopLimitOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("profitId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/profit-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTStopLimitOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Edit Stop Limit Order

        /// <inheritdoc />
        public async Task<WebCallResult> EditStopLimitOrderAsync(long orderId, decimal? triggerProfitPrice = null, decimal? triggerStopPrice = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("profitId", orderId);
            parameters.AddOptional("triggerProfitPrice", triggerProfitPrice);
            parameters.AddOptional("triggerStopPrice", triggerStopPrice);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/update-profit-stop", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Place Track Order

        /// <inheritdoc />
        public async Task<WebCallResult<long>> PlaceTrackOrderAsync(string symbol, OrderSide orderSide, PositionSide positionSide, PositionType positionType, decimal quantity, TrackRange trackRange, decimal callbackValue, PriceType triggerPriceType, decimal? activationPrice = null, string? clientMedia = null, string? clientMediaChannel = null, string? clientOrderId = null, DateTime? expireTime = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddEnum("orderSide", orderSide);
            parameters.AddEnum("positionSide", positionSide);
            parameters.AddEnum("positionType", positionType);
            parameters.Add("origQty", quantity);
            parameters.AddEnum("callback", trackRange);
            parameters.Add("callbackVal", callbackValue);
            parameters.AddEnum("triggerPriceType", triggerPriceType);
            parameters.AddOptional("activationPrice", activationPrice);
            parameters.AddOptional("clientMedia", clientMedia);
            parameters.AddOptional("clientMediaChannel", clientMediaChannel);
            parameters.AddOptional("clientOrderId", clientOrderId);
            parameters.AddOptionalMilliseconds("expireTime", expireTime);
            parameters.Add("media", XTExchange._clientRef);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/create-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            return result.As<long>(result.Data ?? 0);
        }

        #endregion

        #region Cancel Track Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelTrackOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("trackId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Track Order

        /// <inheritdoc />
        public async Task<WebCallResult<XTTrackOrder>> GetTrackOrderAsync(long orderId, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("trackId", orderId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/track-detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTrackOrder>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Track Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTDataPage<XTTrackOrder>>> GetOpenTrackOrdersAsync(string? symbol = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("pageSize", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/track-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTDataPage<XTTrackOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Cancel All Track Orders

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllTrackOrdersAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, "/future/trade/v1/entrust/cancel-all-track", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Closed Track Orders

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTTrackOrder>>> GetClosedTrackOrdersAsync(string? symbol = null, TrackOrderStatus? status = null, long? orderId = null, DateTime? startTime = null, DateTime? endTime = null, int? page = null, int? pageSize = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("id", orderId);
            parameters.AddOptionalEnum("state", status);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("page", page);
            parameters.AddOptional("size", pageSize);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/trade/v1/entrust/profit-list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTTrackOrder>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
