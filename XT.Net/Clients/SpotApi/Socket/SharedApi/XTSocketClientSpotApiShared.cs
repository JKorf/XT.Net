using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XT.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using CryptoExchange.Net;
using System.Linq;

namespace XT.Net.Clients.SpotApi
{
    internal partial class XTSocketClientSpotSharedApi :
        SharedApiBase,
        IXTSocketClientSpotApiShared,
        IXTSocketClientSpotSharedApi
    {
        private readonly XTSocketClientSpotApi _api;

        private const string _topicId = "XTSpot";
        private const string _exchangeName = "XT";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(XTExchange.Metadata, this);

        public XTSocketClientSpotSharedApi(XTSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeBalanceOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions,
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeUserTradeOptions,
                SubscribeSpotOrderOptions
                );
        }

    }
}
