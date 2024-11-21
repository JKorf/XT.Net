using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    public record XTBalances
    {
        /// <summary>
        /// Total btc quantity
        /// </summary>
        [JsonPropertyName("totalBtcAmount")]
        public decimal TotalBtcQuantity { get; set; }
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("assets")]
        public IEnumerable<XTBalance> Assets { get; set; } = Array.Empty<XTBalance>();
    }

    /// <summary>
    /// Balance
    /// </summary>
    public record XTBalance
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Asset id
        /// </summary>
        [JsonPropertyName("currencyId")]
        public long AssetId { get; set; }
        /// <summary>
        /// Frozen quantity
        /// </summary>
        [JsonPropertyName("frozenAmount")]
        public decimal QuantityFrozen { get; set; }
        /// <summary>
        /// Withdrawable balance
        /// </summary>
        [JsonPropertyName("withdraw")]
        public decimal Withdrawable { get; set; }
        /// <summary>
        /// Trade balance
        /// </summary>
        [JsonPropertyName("trade")]
        public decimal Trade { get; set; }
        /// <summary>
        /// Copy trade balance
        /// </summary>
        [JsonPropertyName("copyTrade")]
        public decimal CopyTrade { get; set; }
        /// <summary>
        /// Locked balance
        /// </summary>
        [JsonPropertyName("lock")]
        public decimal Locked { get; set; }
        /// <summary>
        /// Freeze
        /// </summary>
        [JsonPropertyName("freeze")]
        public decimal Freeze { get; set; }
        /// <summary>
        /// Available quantity
        /// </summary>
        [JsonPropertyName("availableAmount")]
        public decimal QuantityAvailable { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("totalAmount")]
        public decimal QuantityTotal { get; set; }
        /// <summary>
        /// Value in BTC
        /// </summary>
        [JsonPropertyName("convertBtcAmount")]
        public decimal ConvertBtcQuantity { get; set; }
        /// <summary>
        /// Value in USDT
        /// </summary>
        [JsonPropertyName("convertUsdtAmount")]
        public decimal ConvertUsdtQuantity { get; set; }
    }


}
