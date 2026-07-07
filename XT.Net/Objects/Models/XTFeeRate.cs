using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Fee info
    /// </summary>
    [SerializationModel]
    public record XTFeeRate
    {
        /// <summary>
        /// ["<c>makerFee</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerFee</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>specialType</c>"] Special type
        /// </summary>
        [JsonPropertyName("specialType")]
        public bool SpecialType { get; set; }
        /// <summary>
        /// ["<c>vipProType</c>"] VIP pro type
        /// </summary>
        [JsonPropertyName("vipProType")]
        public bool VipProType { get; set; }
        /// <summary>
        /// ["<c>stepRateProName</c>"] Step rate pro name
        /// </summary>
        [JsonPropertyName("stepRateProName")]
        public string? StepRateProName { get; set; }
        /// <summary>
        /// ["<c>discountLevel</c>"] Discount level
        /// </summary>
        [JsonPropertyName("discountLevel")]
        public decimal DiscountLevel { get; set; }
        /// <summary>
        /// ["<c>levelReturnDay</c>"] Current level retention days
        /// </summary>
        [JsonPropertyName("levelReturnDay")]
        public int LevelRetention { get; set; }
        /// <summary>
        /// ["<c>totalTradeVolume</c>"] Total trade volume
        /// </summary>
        [JsonPropertyName("totalTradeVolume")]
        public decimal TotalTradeVolume { get; set; }
        /// <summary>
        /// ["<c>uBasedTotalTradeVolume</c>"] USDT-margined trade volume
        /// </summary>
        [JsonPropertyName("uBasedTotalTradeVolume")]
        public decimal UsdtMTradingVolume { get; set; }
        /// <summary>
        /// ["<c>coinBasedTotalTradeVolume</c>"] Coin-margined trade volume
        /// </summary>
        [JsonPropertyName("coinBasedTotalTradeVolume")]
        public decimal CoinMTradingVolume { get; set; }
        /// <summary>
        /// ["<c>walletBalance</c>"] Account equity in USDT
        /// </summary>
        [JsonPropertyName("walletBalance")]
        public decimal Equity { get; set; }
        /// <summary>
        /// ["<c>notProfit</c>"] Unrealized profit and lost
        /// </summary>
        [JsonPropertyName("notProfit")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>nextLvTradeVolume</c>"] Next level trading volume
        /// </summary>
        [JsonPropertyName("nextLvTradeVolume")]
        public decimal NextLevelTradeVolume { get; set; }
        /// <summary>
        /// ["<c>lackTradeVolume</c>"] Trading volume needed for next level
        /// </summary>
        [JsonPropertyName("lackTradeVolume")]
        public decimal NextLevelTradeVolumeRequired { get; set; }
        /// <summary>
        /// ["<c>nextLvWalletBalance</c>"] Next level wallet balance
        /// </summary>
        [JsonPropertyName("nextLvWalletBalance")]
        public decimal NextLevelWalletBalance { get; set; }
        /// <summary>
        /// ["<c>lackWalletBalance</c>"] Next level wallet balance required
        /// </summary>
        [JsonPropertyName("lackWalletBalance")]
        public decimal NextLevelWalletBalanceRequired { get; set; }
        /// <summary>
        /// ["<c>nextLvMakerFee</c>"] Next level maker fee
        /// </summary>
        [JsonPropertyName("nextLvMakerFee")]
        public decimal NextLevelMakerFee { get; set; }
        /// <summary>
        /// ["<c>nextLvTakerFee</c>"] Next level taker fee
        /// </summary>
        [JsonPropertyName("nextLvTakerFee")]
        public decimal NextLevelTakerFee { get; set; }
        /// <summary>
        /// ["<c>feeSource</c>"] Fee source
        /// </summary>
        [JsonPropertyName("feeSource")]
        public string FeeSource { get; set; } = string.Empty;
    }


}
