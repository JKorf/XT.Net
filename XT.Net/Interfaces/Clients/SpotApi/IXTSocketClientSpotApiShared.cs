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
}
