using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Text;

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
