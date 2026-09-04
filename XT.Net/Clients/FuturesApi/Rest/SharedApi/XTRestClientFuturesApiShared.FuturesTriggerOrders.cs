using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XT.Net.Interfaces.Clients.FuturesApi;
using System.Linq;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using CryptoExchange.Net;
using XT.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTRestClientFuturesSharedApi
    {
        #region Place Futures Trigger Order

        async Task<ICallResult<SharedId>> IPlaceFuturesTriggerOrder.PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
            => await PlaceFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public PlaceFuturesTriggerOrderOptions PlaceFuturesTriggerOrderOptions { get; } = new PlaceFuturesTriggerOrderOptions(_exchangeName, false)
        {
            RequiredRequestParameters = new List<ParameterDescription>
            {
                RequestParameter<PlaceFuturesTriggerOrderRequest>.Required(x => x.PositionMode, "PositionMode the account is in", SharedPositionMode.OneWay)
            }
        };

        public async Task<HttpResult<SharedId>> PlaceFuturesTriggerOrderAsync(PlaceFuturesTriggerOrderRequest request, CancellationToken ct)
        {
            var side = GetOrderSide(request);
            var validationError = PlaceFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var result = await _api.Trading.PlaceTriggerOrderAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                side,
                request.OrderPrice == null ? TriggerOrderType.StopMarket : TriggerOrderType.StopLimit,
                quantity: request.Quantity.QuantityInContracts ?? 0,
                stopPrice: request.TriggerPrice,
                timeInForce: GetTriggerTimeInForce(request),
                triggerPriceType: GetTriggerPriceType(request),
                clientOrderId: request.ClientOrderId,
                positionSide: request.PositionSide == SharedPositionSide.Long ? PositionSide.Long : PositionSide.Short,
                orderPrice: request.OrderPrice,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedId>(result);

            // Return
            return HttpResult.Ok(result, new SharedId(result.Data.ToString()));
        }

        #endregion

        private PriceType GetTriggerPriceType(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TriggerPriceType == null || request.TriggerPriceType == SharedTriggerPriceType.LastPrice)
                return PriceType.LastPrice;

            if (request.TriggerPriceType == SharedTriggerPriceType.IndexPrice)
                return PriceType.IndexPrice;

            return PriceType.MarkPrice;
        }

        private TimeInForce GetTriggerTimeInForce(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.TimeInForce == null)
                return request.OrderPrice == null ? TimeInForce.ImmediateOrCancel : TimeInForce.GoodTillCanceled;

            if (request.TimeInForce == SharedTimeInForce.GoodTillCanceled)
                return TimeInForce.GoodTillCanceled;

            if (request.TimeInForce == SharedTimeInForce.FillOrKill)
                return TimeInForce.FillOrKill;

            return TimeInForce.ImmediateOrCancel;
        }

        private OrderSide GetOrderSide(PlaceFuturesTriggerOrderRequest request)
        {
            if (request.PositionSide == SharedPositionSide.Long)
                return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Buy : OrderSide.Sell;

            return request.OrderDirection == SharedTriggerOrderDirection.Enter ? OrderSide.Sell : OrderSide.Buy;
        }
        #region Get Futures Trigger Order

        async Task<ICallResult<SharedFuturesTriggerOrder>> IGetFuturesTriggerOrder.GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
            => await GetFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public GetFuturesTriggerOrderOptions GetFuturesTriggerOrderOptions { get; } = new GetFuturesTriggerOrderOptions(_exchangeName, true)
        {
        };
        public async Task<HttpResult<SharedFuturesTriggerOrder>> GetFuturesTriggerOrderAsync(GetOrderRequest request, CancellationToken ct)
        {
            var validationError = GetFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedFuturesTriggerOrder>(Exchange, ArgumentError.Invalid(nameof(GetOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.GetTriggerOrderAsync(orderId, ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedFuturesTriggerOrder>(order);

            return HttpResult.Ok(order, new SharedFuturesTriggerOrder(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, order.Data.Symbol),
                order.Data.Symbol,
                order.Data.OrderId.ToString(),
                order.Data.TriggerOrderType == TriggerOrderType.StopLimit ? SharedOrderType.Limit : SharedOrderType.Market,
                ParseOrderDirection(order.Data),
                ParseTriggerOrderStatus(order.Data),
                order.Data.StopPrice,
                order.Data.PositionSide == PositionSide.Short ? SharedPositionSide.Short : SharedPositionSide.Long,
                order.Data.CreateTime)
            {
                PlacedOrderId = order.Data.OrderId.ToString(),
                AveragePrice = order.Data.AveragePrice == 0 ? null : order.Data.AveragePrice,
                OrderPrice = order.Data.Price == 0 ? null : order.Data.Price,
                OrderQuantity = new SharedOrderQuantity(contractQuantity: order.Data.Quantity),
                ClientOrderId = order.Data.ClientOrderId
            });
        }

        #endregion

        private SharedTriggerOrderDirection? ParseOrderDirection(XTTriggerOrder data)
        {
            if (data.PositionSide == PositionSide.Long)
                return data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Enter : SharedTriggerOrderDirection.Exit;

            return data.OrderSide == OrderSide.Buy ? SharedTriggerOrderDirection.Exit : SharedTriggerOrderDirection.Enter;
        }

        private SharedTriggerOrderStatus ParseTriggerOrderStatus(XTTriggerOrder data)
        {
            if (data.Status == TriggerOrderStatus.Expired
                || data.Status == TriggerOrderStatus.PlatformRevocation
                || data.Status == TriggerOrderStatus.UserRevocation)
            {
                return SharedTriggerOrderStatus.CanceledOrRejected;
            }

            if (data.Status == TriggerOrderStatus.NotTriggered
                || data.Status == TriggerOrderStatus.Triggering
                || data.Status == TriggerOrderStatus.Unfinished)
            {
                return SharedTriggerOrderStatus.Active;
            }

            if (data.Status == TriggerOrderStatus.Triggered)
                return SharedTriggerOrderStatus.Triggered;

            return SharedTriggerOrderStatus.Unknown;
        }
        #region Cancel Futures Trigger Order

        async Task<ICallResult<SharedId>> ICancelFuturesTriggerOrder.CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
            => await CancelFuturesTriggerOrderAsync(request, ct).ConfigureAwait(false);

        public CancelFuturesTriggerOrderOptions CancelFuturesTriggerOrderOptions { get; } = new CancelFuturesTriggerOrderOptions(_exchangeName, true);
        public async Task<HttpResult<SharedId>> CancelFuturesTriggerOrderAsync(CancelOrderRequest request, CancellationToken ct)
        {
            var validationError = CancelFuturesTriggerOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            if (!long.TryParse(request.OrderId, out var orderId))
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid(nameof(CancelOrderRequest.OrderId), "Invalid order id"));

            var order = await _api.Trading.CancelTriggerOrderAsync(
                orderId,
                ct: ct).ConfigureAwait(false);
            if (!order.Success)
                return HttpResult.Fail<SharedId>(order);

            return HttpResult.Ok(order, new SharedId(request.OrderId));
        }

        #endregion
    }
}
