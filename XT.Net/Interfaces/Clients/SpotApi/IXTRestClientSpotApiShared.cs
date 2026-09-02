using CryptoExchange.Net.SharedApis;

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
        IOrderBookRestClient,
        IRecentTradeRestClient,
        IWithdrawalRestClient,
        IWithdrawRestClient,
        ISpotTickerRestClient,
        ISpotSymbolRestClient,
        ISpotOrderRestClient,
        IFeeRestClient,
        IBookTickerRestClient,
        ITransferRestClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IXTRestClientSpotSharedApi
        : IGetAssetRest,
        IGetAllAssetsRest,
        IGetBalancesRest,
        IGetDepositAddressesRest,
        IGetDepositHistoryRest,
        IGetKlinesRest,
        IGetOrderBookRest,
        IGetRecentTradesRest,
        IGetWithdrawalHistoryRest,
        IWithdrawRest,
        IGetSpotTickerRest,
        IGetAllSpotTickersRest,
        IGetSpotSymbolsRest,
        IPlaceSpotOrderRest,
        IGetSpotOrderRest,
        IGetOpenSpotOrdersRest,
        IGetClosedSpotOrdersRest,
        IGetSpotOrderTradesRest,
        IGetSpotUserTradeHistoryRest,
        ICancelSpotOrderRest,
        IGetFeesRest,
        IGetBookTickerRest,
        ITransferRest
    {
    }
}
