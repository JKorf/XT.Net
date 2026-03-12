using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Trade role
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TradeRole>))]
    public enum TradeRole
    {
        /// <summary>
        /// ["<c>maker</c>"] Maker
        /// </summary>
        [Map("maker")]
        Maker,
        /// <summary>
        /// ["<c>taker</c>"] Taker
        /// </summary>
        [Map("taker")]
        Taker
    }
}
