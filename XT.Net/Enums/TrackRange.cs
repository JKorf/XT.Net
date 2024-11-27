using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Track callback config
    /// </summary>
    public enum TrackRange
    {
        /// <summary>
        /// Fixed
        /// </summary>
        [Map("FIXED")]
        Fixed,
        /// <summary>
        /// Proportion
        /// </summary>
        [Map("PROPORTION")]
        Proportion
    }
}
