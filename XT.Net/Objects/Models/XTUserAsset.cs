using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// User asset
    /// </summary>
    public record XTUserAsset
    {
        /// <summary>
        /// Available balance
        /// </summary>
        [JsonPropertyName("availableBalance")]
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// Crossed margin
        /// </summary>
        [JsonPropertyName("crossedMargin")]
        public decimal CrossedMargin { get; set; }
        /// <summary>
        /// Bonus
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal Bonus { get; set; }
        /// <summary>
        /// Coupon
        /// </summary>
        [JsonPropertyName("coupon")]
        public decimal Coupon { get; set; }
        /// <summary>
        /// Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
    }


}
