using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Order side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderSide>))]
    public enum OrderSide
    {
        /// <summary>
        /// ["<c>BUY</c>"] Buy
        /// </summary>
        [Map("BUY", "ASK")]
        Buy,
        /// <summary>
        /// ["<c>SELL</c>"] Sell
        /// </summary>
        [Map("SELL", "BID")]
        Sell
    }
}
