﻿using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    public enum OrderSide
    {
        /// <summary>
        /// Buy
        /// </summary>
        [Map("BUY", "ASK")]
        Buy,
        /// <summary>
        /// Sell
        /// </summary>
        [Map("SELL", "BID")]
        Sell
    }
}
