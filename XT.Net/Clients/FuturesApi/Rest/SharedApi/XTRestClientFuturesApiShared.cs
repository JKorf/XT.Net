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
    internal partial class XTRestClientFuturesSharedApi :
        SharedApiBase,
        IXTRestClientFuturesApiShared,
        IXTRestClientFuturesSharedApi
    {
        private readonly XTRestClientFuturesApi _api;

        private const string _topicId = "XTFutures";
        private const string _exchangeName = "XT";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(XTExchange.Metadata, this);

        public XTRestClientFuturesSharedApi(XTRestClientFuturesApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  api is XTRestClientUsdtFuturesApi
                        ? new[] { TradingMode.PerpetualLinear, TradingMode.DeliveryLinear }
                        : [TradingMode.PerpetualInverse, TradingMode.DeliveryInverse],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetBalancesOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetFundingRateHistoryOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBookTickerOptions,
                GetOpenInterestOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                CancelFuturesOrderOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFeeOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                GetMarkPriceOptions,
                GetMarkPricesOptions,
                GetIndexPriceOptions,
                GetIndexPricesOptions,
                EditFuturesOrderOptions,
                CancelAllFuturesOrdersOptions,
                CancelAllFuturesSymbolOrdersOptions
                );
        }

    }
}
