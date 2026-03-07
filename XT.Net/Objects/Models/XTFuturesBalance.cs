using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record XTFuturesBalance
    {
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>userId</c>"] User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
        /// <summary>
        /// ["<c>coin</c>"] Asset
        /// </summary>
        [JsonPropertyName("coin")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlyingType</c>"] Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// ["<c>walletBalance</c>"] Wallet balance
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal WalletBalance { get; set; }
        /// <summary>
        /// ["<c>openOrderMarginFrozen</c>"] Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>crossedMargin</c>"] Crossed margin
        /// </summary>
        [JsonPropertyName("crossedMargin")]
        public decimal CrossedMargin { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Net asset balance
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>totalAmount</c>"] Margin balance
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal TotalQuantity { get; set; }
        /// <summary>
        /// ["<c>convertBtcAmount</c>"] Wallet balance in BTC
        /// </summary>
        [JsonPropertyName("convertBtcAmount")]
        public decimal ConvertBtcQuantity { get; set; }
        /// <summary>
        /// ["<c>convertUsdtAmount</c>"] Wallet balance in USDT
        /// </summary>
        [JsonPropertyName("convertUsdtAmount")]
        public decimal ConvertUsdtQuantity { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit and loss
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Pnl { get; set; }
        /// <summary>
        /// ["<c>notProfit</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("notProfit")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>bonus</c>"] Bonus
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal Bonus { get; set; }
        /// <summary>
        /// ["<c>coupon</c>"] Coupon
        /// </summary>
        [JsonPropertyName("coupon")]
        public decimal Coupon { get; set; }
    }


}
