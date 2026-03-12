using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Position side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionSide>))]
    public enum PositionSide
    {
        /// <summary>
        /// ["<c>LONG</c>"] Long
        /// </summary>
        [Map("LONG")]
        Long,
        /// <summary>
        /// ["<c>SHORT</c>"] Short
        /// </summary>
        [Map("SHORT")]
        Short
    }
}
