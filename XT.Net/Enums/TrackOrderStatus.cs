using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Track order status
    /// </summary>
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
