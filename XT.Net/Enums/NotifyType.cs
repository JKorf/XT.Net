using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Notification type
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Warning, about to be levelled
        /// </summary>
        [Map("WARN")]
        Warning,
        /// <summary>
        /// Partially liquidation
        /// </summary>
        [Map("PARTIAL")]
        PartialLiquidation,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// ADL
        /// </summary>
        [Map("ADL")]
        AutoDeleverage,
    }

}
