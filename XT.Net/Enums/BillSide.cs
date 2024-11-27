using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Bill side
    /// </summary>
    public enum BillSide
    {
        /// <summary>
        /// Transfer in
        /// </summary>
        [Map("ADD")]
        TransferIn,
        /// <summary>
        /// Transfer out
        /// </summary>
        [Map("SUB")]
        TransferOut
    }
}
