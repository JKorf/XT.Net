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
        /// ["<c>PRICE</c>"] Price filter
        /// </summary>
        [Map("PRICE")]
        Price,
        /// <summary>
        /// ["<c>QUANTITY</c>"] Quantity filter
        /// </summary>
        [Map("QUANTITY")]
        Quantity,
        /// <summary>
        /// ["<c>QUOTE_QTY</c>"] Quote quantity filter
        /// </summary>
        [Map("QUOTE_QTY")]
        QuoteQuantity,
        /// <summary>
        /// ["<c>PROTECTION_LIMIT</c>"] Limit order protection
        /// </summary>
        [Map("PROTECTION_LIMIT")]
        ProtectionLimit,
        /// <summary>
        /// ["<c>PROTECTION_MARKET</c>"] Market order protection
        /// </summary>
        [Map("PROTECTION_MARKET")]
        ProtectionMarket,
        /// <summary>
        /// ["<c>PROTECTION_ONLINE</c>"] Filter for when symbol comes online
        /// </summary>
        [Map("PROTECTION_ONLINE")]
        ProtectionOnline
    }
}
