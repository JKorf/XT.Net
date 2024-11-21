using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Symbol filter type
    /// </summary>
    public enum SymbolFilterType
    {
        /// <summary>
        /// Price filter
        /// </summary>
        [Map("PRICE")]
        Price,
        /// <summary>
        /// Quantity filter
        /// </summary>
        [Map("QUANTITY")]
        Quantity,
        /// <summary>
        /// Quote quantity filter
        /// </summary>
        [Map("QUOTE_QTY")]
        QuoteQuantity,
        /// <summary>
        /// Limit order protection
        /// </summary>
        [Map("PROTECTION_LIMIT")]
        ProtectionLimit,
        /// <summary>
        /// Market order protection
        /// </summary>
        [Map("PROTECTION_MARKET")]
        ProtectionMarket,
        /// <summary>
        /// Filter for when symbol comes online
        /// </summary>
        [Map("PROTECTION_ONLINE")]
        ProtectionOnline
    }
}
