using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IXTRestClientSpotApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IDepositRestClient,
        IKlineRestClient,
        IListenKeyRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        ISpotTickerRestClient,
        ISpotSymbolRestClient,
        ISpotOrderRestClient,
        IFeeRestClient,
        IBookTickerRestClient
    {
    }
}
