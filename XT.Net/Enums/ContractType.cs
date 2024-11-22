using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Perpetual
        /// </summary>
        [Map("PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Next quarter
        /// </summary>
        [Map("NEXT_QUARTER")]
        NextQuarter,
        /// <summary>
        /// Current quarter
        /// </summary>
        [Map("CURRENT_QUARTER")]
        CurrentQuarter
    }
}
