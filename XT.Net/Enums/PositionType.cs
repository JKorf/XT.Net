using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Position type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PositionType>))]
    public enum PositionType
    {
        /// <summary>
        /// Cross
        /// </summary>
        [Map("CROSSED")]
        Cross,
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}
