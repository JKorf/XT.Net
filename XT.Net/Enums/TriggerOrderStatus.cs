using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Trigger order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TriggerOrderStatus>))]
    public enum TriggerOrderStatus
    {
        /// <summary>
        /// Not triggered
        /// </summary>
        [Map("NOT_TRIGGERED")]
        NotTriggered,
        /// <summary>
        /// Triggering
        /// </summary>
        [Map("TRIGGERING")]
        Triggering,
        /// <summary>
        /// Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered,
        /// <summary>
        /// User revocation
        /// </summary>
        [Map("USER_REVOCATION")]
        UserRevocation,
        /// <summary>
        /// Platform revocation
        /// </summary>
        [Map("PLATFORM_REVOCATION")]
        PlatformRevocation,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Unfinished
        /// </summary>
        [Map("UNFINISHED")]
        Unfinished,
        /// <summary>
        /// History
        /// </summary>
        [Map("HISTORY")]
        History,
    }

}
