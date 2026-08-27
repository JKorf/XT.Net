using CryptoExchange.Net.SharedApis;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IXTSocketClientSpotApiShared :
        IBalanceSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        IUserTradeSocketClient,
        ISpotOrderSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IXTSocketClientSpotSharedApi :
        ISubscribeBalancesOperation,
        ISubscribeKlinesOperation,
        ISubscribeOrderBookOperation,
        ISubscribeTickerOperation,
        ISubscribeTradesOperation,
        ISubscribeUserTradesOperation,
        ISubscribeSpotOrdersOperation
    {
    }
}
