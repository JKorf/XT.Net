using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Symbol filter type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolFilterType>))]
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
