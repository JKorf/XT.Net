using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Bill type
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// Transfer
        /// </summary>
        [Map("EXCHANGE")]
        Transfer,
        /// <summary>
        /// Offset profit and loss
        /// </summary>
        [Map("CLOSE_POSITION")]
        ClosePosition,
        /// <summary>
        /// Position takeover
        /// </summary>
        [Map("TAKE_OVER")]
        TakeOver,
        /// <summary>
        /// Liquidation management fee
        /// </summary>
        [Map("QIANG_PING_MANAGER")]
        LiquidationManagementFee,
        /// <summary>
        /// Fund fee
        /// </summary>
        [Map("FUND")]
        FundingFee,
        /// <summary>
        /// Fee (Open position, liquidation, forced liquidation)
        /// </summary>
        [Map("FEE")]
        Fee,
        /// <summary>
        /// Adl
        /// </summary>
        [Map("ADL")]
        AutoDeleverage,
        /// <summary>
        /// Position merge
        /// </summary>
        [Map("MERGE")]
        PositionMerge,
    }

}
