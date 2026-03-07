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
        /// ["<c>currency</c>"] Asset name
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>supportChains</c>"] Supported networks
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
        /// ["<c>chain</c>"] Network name
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositEnabled</c>"] Deposit enabled
        /// </summary>
        [JsonPropertyName("depositEnabled")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawEnabled</c>"] Withdraw enabled
        /// </summary>
        [JsonPropertyName("withdrawEnabled")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>withdrawFeeAmount</c>"] Withdraw fee
        /// </summary>
        [JsonPropertyName("withdrawFeeAmount")]
        public decimal WithdrawFeeQuantity { get; set; }
        /// <summary>
        /// ["<c>withdrawFeeCurrency</c>"] Withdraw fee asset
        /// </summary>
        [JsonPropertyName("withdrawFeeCurrency")]
        public string WithdrawFeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdrawFeeCurrencyId</c>"] Withdraw fee asset id
        /// </summary>
        [JsonPropertyName("withdrawFeeCurrencyId")]
        public int WithdrawFeeAssetId { get; set; }
        /// <summary>
        /// ["<c>withdrawMinAmount</c>"] Withdraw min quantity
        /// </summary>
        [JsonPropertyName("withdrawMinAmount")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// ["<c>depositFeeRate</c>"] Deposit fee rate
        /// </summary>
        [JsonPropertyName("depositFeeRate")]
        public decimal DepositFeeRate { get; set; }
        /// <summary>
        /// ["<c>contract</c>"] Contract
        /// </summary>
        [JsonPropertyName("contract")]
        public string Contract { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>depositMinAmount</c>"] Min deposit quantity
        /// </summary>
        [JsonPropertyName("depositMinAmount")]
        public decimal? MinDepositQuantity { get; set; }
        /// <summary>
        /// ["<c>depositConfirmations</c>"] Deposit confirmations required
        /// </summary>
        [JsonPropertyName("depositConfirmations")]
        public int? DepositConfirmations { get; set; }
        /// <summary>
        /// ["<c>withdrawPrecision</c>"] Withdraw quantity precision
        /// </summary>
        [JsonPropertyName("withdrawPrecision")]
        public int WithdrawPrecision { get; set; }
    }


}
