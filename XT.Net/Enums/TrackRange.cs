using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Track callback config
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TrackRange>))]
    public enum TrackRange
    {
        /// <summary>
        /// ["<c>FIXED</c>"] Fixed
        /// </summary>
        [Map("FIXED")]
        Fixed,
        /// <summary>
        /// ["<c>PROPORTION</c>"] Proportion
        /// </summary>
        [Map("PROPORTION")]
        Proportion
    }
}
