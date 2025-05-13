using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Track order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TrackOrderStatus>))]
    public enum TrackOrderStatus
    {
        /// <summary>
        /// Inactivated
        /// </summary>
        [Map("NOT_ACTIVATION")]
        NotActivation,
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
        /// Platform rejects
        /// </summary>
        [Map("PLATFORM_REVOCATION")]
        PlatformRevocation,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// Delegation failed
        /// </summary>
        [Map("DELEGATION_FAILED")]
        DelegationFailed,
    }

}
