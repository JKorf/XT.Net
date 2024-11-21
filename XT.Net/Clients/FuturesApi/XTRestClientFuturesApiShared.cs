using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;
using XT.Net.Interfaces.Clients.FuturesApi;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTRestClientFuturesApi : IXTRestClientFuturesApiShared
    {
        public string Exchange => "XT";

        public TradingMode[] SupportedTradingModes => new[] { TradingMode.Spot };

        public void SetDefaultExchangeParameter(string key, object value) => ExchangeParameters.SetStaticParameter(Exchange, key, value);
        public void ResetDefaultExchangeParameters() => ExchangeParameters.ResetStaticParameters();
    }
}
