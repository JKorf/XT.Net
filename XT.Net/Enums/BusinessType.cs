using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// Normal spot
        /// </summary>
        [Map("SPOT")]
        Spot,
        /// <summary>
        /// Margin
        /// </summary>
        [Map("LEVER")]
        Leverage,

        /// <summary>
        /// Finanace account
        /// </summary>
        [Map("FINANCE")]
        Finance,
        /// <summary>
        /// USDT-M futures
        /// </summary>
        [Map("FUTURES_U")]
        UsdtFutures,
        /// <summary>
        /// Coin-M futures
        /// </summary>
        [Map("FUTURES_C")]
        CoinFutures
    }
}
