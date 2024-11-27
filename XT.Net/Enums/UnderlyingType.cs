using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Underlying type
    /// </summary>
    public enum UnderlyingType
    {
        /// <summary>
        /// Usdt based
        /// </summary>
        [Map("U_BASED", "2")]
        UsdtBased,
        /// <summary>
        /// Coin based
        /// </summary>
        [Map("C_BASED", "COIN_BASED", "1")]
        CoinBased
    }
}
