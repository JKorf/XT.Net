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
        /// Normal order
        /// </summary>
        [Map("DEFAULT")]
        Normal,
        /// <summary>
        /// Trigger order
        /// </summary>
        [Map("ENTRUST")]
        TriggerOrder,
        /// <summary>
        /// TP/SL order
        /// </summary>
        [Map("PROFIT")]
        TpSlOrder
    }
}
