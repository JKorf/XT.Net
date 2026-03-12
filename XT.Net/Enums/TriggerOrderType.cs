using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Take profit / stop loss order type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderType>))]
    public enum TriggerOrderType
    {
        /// <summary>
        /// ["<c>TAKE_PROFIT</c>"] Take profit
        /// </summary>
        [Map("TAKE_PROFIT")]
        TakeProfitLimit,
        /// <summary>
        /// ["<c>STOP</c>"] Stop
        /// </summary>
        [Map("STOP")]
        StopLimit,
        /// <summary>
        /// ["<c>TAKE_PROFIT_MARKET</c>"] Take profit market
        /// </summary>
        [Map("TAKE_PROFIT_MARKET")]
        TakeProfitMarket,
        /// <summary>
        /// ["<c>STOP_MARKET</c>"] Stop market
        /// </summary>
        [Map("STOP_MARKET")]
        StopMarket,
    }

}
