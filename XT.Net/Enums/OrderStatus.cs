using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// Partially filled
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
    }

}
