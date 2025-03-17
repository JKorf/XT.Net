using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Next quarter
        /// </summary>
        [Map("NEXT_QUARTER")]
        NextQuarter,
        /// <summary>
        /// Current quarter
        /// </summary>
        [Map("CURRENT_QUARTER")]
        CurrentQuarter
    }
}
