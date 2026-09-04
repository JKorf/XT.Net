using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XT.Net.Clients.FuturesApi;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.SpotApi
{
    internal partial class XTRestClientSpotSharedApi :
        SharedApiBase,
        IXTRestClientSpotApiShared,
        IXTRestClientSpotSharedApi
    {
        private readonly XTRestClientSpotApi _api;

        private const string _topicId = "XTSpot";
        private const string _exchangeName = "XT";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(XTExchange.Metadata, this);

        public XTRestClientSpotSharedApi(XTRestClientSpotApi api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetAssetOptions,
                GetAllAssetsOptions,
                GetBalancesOptions,
                GetDepositHistoryOptions,
                GetDepositAddressesOptions,
                GetKlinesOptions,
                GetOrderBookOptions,
                GetRecentTradesOptions,
                GetWithdrawalHistoryOptions,
                WithdrawOptions,
                GetSpotTickerOptions,
                GetAllSpotTickersOptions,
                GetBookTickerOptions,
                GetSpotSymbolsOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                GetSpotUserTradeHistoryOptions,
                CancelSpotOrderOptions,
                GetFeeOptions,
                TransferOptions,
                CancelAllSpotOrdersOptions,
                CancelAllSpotSymbolOrdersOptions
                );
        }

    }
}
