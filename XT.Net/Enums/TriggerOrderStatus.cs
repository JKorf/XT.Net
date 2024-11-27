using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Trigger order status
    /// </summary>
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
