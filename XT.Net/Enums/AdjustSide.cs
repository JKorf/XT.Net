using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Adjust side
    /// </summary>
    public enum AdjustSide
    {
        /// <summary>
        /// Add
        /// </summary>
        [Map("ADD")]
        Add,
        /// <summary>
        /// Remove
        /// </summary>
        [Map("SUB")]
        Remove
    }
}
