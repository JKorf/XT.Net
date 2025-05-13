using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Account status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountStatus>))]
    public enum AccountStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("0")]
        Normal

        // Other values not documented..
    }
}
