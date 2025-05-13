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
        /// Previous
        /// </summary>
        [Map("PREV")]
        Previous,
        /// <summary>
        /// Next
        /// </summary>
        [Map("NEXT")]
        Next
    }
}
