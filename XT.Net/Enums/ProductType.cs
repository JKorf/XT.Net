﻿using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// Perpetual
        /// </summary>
        [Map("perpetual", "PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures", "PREDICT")]
        Futures
    }
}
