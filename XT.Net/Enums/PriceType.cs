using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    public enum PriceType
    {
        /// <summary>
        /// Index price
        /// </summary>
        [Map("INDEX_PRICE", "1")]
        IndexPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE", "2")]
        MarkPrice,
        /// <summary>
        /// Last price
        /// </summary>
        [Map("LATEST_PRICE", "3")]
        LastPrice
    }
}
