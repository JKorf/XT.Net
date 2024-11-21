using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Page direction
    /// </summary>
    public enum PageDirection
    {
        /// <summary>
        /// Previous
        /// </summary>
        [Map("PREV")]
        Previous,
        /// <summary>
        /// Next
        /// </summary>
        [Map("NEXT")]
        Next
    }
}
