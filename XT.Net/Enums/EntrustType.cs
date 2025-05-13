using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Entrust type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EntrustType>))]
    public enum EntrustType
    {
        /// <summary>
        /// Take profit limit order
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfitLimit,
        /// <summary>
        /// Stop limit order
        /// </summary>
        [Map("STOP")]
        StopLimit,
        /// <summary>
        /// Taker profit market order
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
        /// <summary>
        /// Stop market order
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
        /// <summary>
        /// Trailing stop market order
        /// </summary>
        [Map("TRAILING_STOP_MARKET")]
        TrailingStopMarket
    }
}
