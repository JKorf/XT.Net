using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Networks
    /// </summary>
    [SerializationModel]
    public record XTAssetNetworks
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Supported networks
        /// </summary>
        [JsonPropertyName("supportChains")]
        public XTAssetNetwork[] Networks { get; set; } = Array.Empty<XTAssetNetwork>();
    }

    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record XTAssetNetwork
    {
        /// <summary>
        /// Network name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnabled")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawEnabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Withdraw fee
        /// </summary>
        [JsonPropertyName("withdrawFeeAmount")]
        public decimal WithdrawFeeQuantity { get; set; }
        /// <summary>
        /// Withdraw fee asset
        /// </summary>
        [JsonPropertyName("withdrawFeeCurrency")]
        public string WithdrawFeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Withdraw fee asset id
        /// </summary>
        [JsonPropertyName("withdrawFeeCurrencyId")]
        public int WithdrawFeeAssetId { get; set; }
        /// <summary>
        /// Withdraw min quantity
        /// </summary>
        [JsonPropertyName("withdrawMinAmount")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// Deposit fee rate
        /// </summary>
        [JsonPropertyName("depositFeeRate")]
        public decimal DepositFeeRate { get; set; }
        /// <summary>
        /// Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// Min deposit quantity
        /// </summary>
        [JsonPropertyName("depositMinAmount")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// Deposit confirmations required
        /// </summary>
        [JsonPropertyName("depositConfirmations")]
        public int? DepositConfirmations { get; set; }
        /// <summary>
        /// Withdraw quantity precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public int WithdrawPrecision { get; set; }
    }


}
