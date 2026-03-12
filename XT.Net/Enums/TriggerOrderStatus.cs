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
        /// ["<c>NOT_TRIGGERED</c>"] Not triggered
        /// </summary>
        [Map("NOT_TRIGGERED")]
        NotTriggered,
        /// <summary>
        /// ["<c>TRIGGERING</c>"] Triggering
        /// </summary>
        [Map("TRIGGERING")]
        Triggering,
        /// <summary>
        /// ["<c>TRIGGERED</c>"] Triggered
        /// </summary>
        [Map("TRIGGERED")]
        Triggered,
        /// <summary>
        /// ["<c>USER_REVOCATION</c>"] User revocation
        /// </summary>
        [Map("USER_REVOCATION")]
        UserRevocation,
        /// <summary>
        /// ["<c>PLATFORM_REVOCATION</c>"] Platform revocation
        /// </summary>
        [Map("PLATFORM_REVOCATION")]
        PlatformRevocation,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>UNFINISHED</c>"] Unfinished
        /// </summary>
        [Map("UNFINISHED")]
        Unfinished,
        /// <summary>
        /// ["<c>HISTORY</c>"] History
        /// </summary>
        [Map("HISTORY")]
        History,
    }

}
