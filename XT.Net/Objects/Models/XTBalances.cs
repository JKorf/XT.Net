using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    [SerializationModel]
    public record XTBalances
    {
        /// <summary>
        /// ["<c>totalBtcAmount</c>"] Total btc quantity
        /// </summary>
        [JsonPropertyName("totalBtcAmount")]
        public decimal TotalBtcQuantity { get; set; }
        /// <summary>
        /// ["<c>assets</c>"] Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public XTBalance[] Assets { get; set; } = Array.Empty<XTBalance>();
    }

    /// <summary>
    /// Balance
    /// </summary>
    [SerializationModel]
    public record XTBalance
    {
        /// <summary>
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currencyId</c>"] Asset id
        /// </summary>
        [JsonPropertyName("currencyId")]
        public long AssetId { get; set; }
        /// <summary>
        /// ["<c>frozenAmount</c>"] Frozen quantity
        /// </summary>
        [JsonPropertyName("frozenAmount")]
        public decimal QuantityFrozen { get; set; }
        /// <summary>
        /// ["<c>withdraw</c>"] Withdrawable balance
        /// </summary>
        [JsonPropertyName("withdraw")]
        public decimal Withdrawable { get; set; }
        /// <summary>
        /// ["<c>trade</c>"] Trade balance
        /// </summary>
        [JsonPropertyName("trade")]
        public decimal Trade { get; set; }
        /// <summary>
        /// ["<c>copyTrade</c>"] Copy trade balance
        /// </summary>
        [JsonPropertyName("copyTrade")]
        public decimal CopyTrade { get; set; }
        /// <summary>
        /// ["<c>lock</c>"] Locked balance
        /// </summary>
        [JsonPropertyName("lock")]
        public decimal Locked { get; set; }
        /// <summary>
        /// ["<c>freeze</c>"] Freeze
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// ["<c>availableAmount</c>"] Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal QuantityAvailable { get; set; }
        /// <summary>
        /// ["<c>totalAmount</c>"] Total quantity
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal QuantityTotal { get; set; }
        /// <summary>
        /// ["<c>convertBtcAmount</c>"] Value in BTC
        /// </summary>
        [JsonPropertyName("convertBtcAmount")]
        public decimal ConvertBtcQuantity { get; set; }
        /// <summary>
        /// ["<c>convertUsdtAmount</c>"] Value in USDT
        /// </summary>
        [JsonPropertyName("convertUsdtAmount")]
        public decimal ConvertUsdtQuantity { get; set; }
    }


}
