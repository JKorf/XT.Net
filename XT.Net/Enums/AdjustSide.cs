using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Adjust side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AdjustSide>))]
    public enum AdjustSide
    {
        /// <summary>
        /// Add
        /// </summary>
        [Map("ADD")]
        Add,
        /// <summary>
        /// Remove
        /// </summary>
        [Map("SUB")]
        Remove
    }
}
