using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Page direction
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PageDirection>))]
    public enum PageDirection
    {
        /// <summary>
        /// ["<c>PREV</c>"] Previous
        /// </summary>
        [Map("PREV")]
        Previous,
        /// <summary>
        /// ["<c>NEXT</c>"] Next
        /// </summary>
        [Map("NEXT")]
        Next
    }
}
