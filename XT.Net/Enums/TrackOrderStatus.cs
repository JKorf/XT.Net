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
        /// ["<c>NOT_ACTIVATION</c>"] Inactivated
        /// </summary>
        [Map("NOT_ACTIVATION")]
        NotActivation,
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
        /// ["<c>PLATFORM_REVOCATION</c>"] Platform rejects
        /// </summary>
        [Map("PLATFORM_REVOCATION")]
        PlatformRevocation,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
        /// <summary>
        /// ["<c>DELEGATION_FAILED</c>"] Delegation failed
        /// </summary>
        [Map("DELEGATION_FAILED")]
        DelegationFailed,
    }

}
