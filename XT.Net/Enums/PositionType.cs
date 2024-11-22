using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Position type
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// Cross
        /// </summary>
        [Map("CROSSED")]
        Cross,
        /// <summary>
        /// Isolated
        /// </summary>
        [Map("ISOLATED")]
        Isolated
    }
}
