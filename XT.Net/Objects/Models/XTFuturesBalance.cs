using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record XTFuturesBalance
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Crossed margin
        /// </summary>
        [JsonPropertyName("crossedMargin")]
        public decimal CrossedMargin { get; set; }
        /// <summary>
        /// Net asset balance
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// Wallet balance in BTC
        /// </summary>
        [JsonPropertyName("convertBtcAmount")]
        public decimal ConvertBtcQuantity { get; set; }
        /// <summary>
        /// Wallet balance in USDT
        /// </summary>
        [JsonPropertyName("convertUsdtAmount")]
        public decimal ConvertUsdtQuantity { get; set; }
        /// <summary>
        /// Profit and loss
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("notProfit")]
        public decimal UnrealizedPnl { get; set; }
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
    }


}
