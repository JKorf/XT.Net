using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IXTRestClientFuturesApiShared :
        IBalanceRestClient,
        IKlineRestClient,
        IListenKeyRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IFundingRateRestClient,
        IFuturesSymbolRestClient,
        IFuturesTickerRestClient,
        ILeverageRestClient,
        IOpenInterestRestClient,
        IFuturesOrderRestClient,
        IFeeRestClient,
        IFuturesTriggerOrderRestClient
    {
    }
}
