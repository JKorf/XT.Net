using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Source type
    /// </summary>
    public enum SourceType
    {
        /// <summary>
        /// Normal order
        /// </summary>
        [Map("DEFAULT")]
        Normal,
        /// <summary>
        /// Trigger order
        /// </summary>
        [Map("ENTRUST")]
        TriggerOrder,
        /// <summary>
        /// TP/SL order
        /// </summary>
        [Map("PROFIT")]
        TpSlOrder
    }
}
