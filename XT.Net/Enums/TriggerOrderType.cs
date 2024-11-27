using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Take profit / stop loss order type
    /// </summary>
    public enum TriggerOrderType
    {
        /// <summary>
        /// Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfitLimit,
        /// <summary>
        /// Stop
        /// </summary>
        [Map("STOP")]
        StopLimit,
        /// <summary>
        /// Take profit market
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
        /// <summary>
        /// Stop market
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
    }

}
