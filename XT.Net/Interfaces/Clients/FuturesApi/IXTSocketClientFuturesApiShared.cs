using CryptoExchange.Net.SharedApis;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IXTSocketClientFuturesApiShared :
        IBalanceSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient,
        ITickerSocketClient,
        ITradeSocketClient,
        IUserTradeSocketClient,
        IFuturesOrderSocketClient,
        IPositionSocketClient
    {
    }

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IXTSocketClientFuturesSharedApi :
        ISubscribeBalancesSocket,
        ISubscribeKlinesSocket,
        ISubscribeOrderBookSocket,
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeUserTradesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket
    {
    }
}
