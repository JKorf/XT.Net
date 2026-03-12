using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Source type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SourceType>))]
    public enum SourceType
    {
        /// <summary>
        /// ["<c>DEFAULT</c>"] Normal order
        /// </summary>
        [Map("DEFAULT")]
        Normal,
        /// <summary>
        /// ["<c>ENTRUST</c>"] Trigger order
        /// </summary>
        [Map("ENTRUST")]
        TriggerOrder,
        /// <summary>
        /// ["<c>PROFIT</c>"] TP/SL order
        /// </summary>
        [Map("PROFIT")]
        TpSlOrder
    }
}
