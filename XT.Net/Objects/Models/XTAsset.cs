using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Assets
    /// </summary>
    [SerializationModel]
    public record XTAssets
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Version of the data
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// Assets
        /// </summary>
        [JsonPropertyName("currencies")]
        public XTAsset[] Assets { get; set; } = [];
    }

    /// <summary>
    /// Asset name
    /// </summary>
    [SerializationModel]
    public record XTAsset
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Full name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Logo
        /// </summary>
        [JsonPropertyName("logo")]
        public string? Logo { get; set; }
        /// <summary>
        /// Cmc link
        /// </summary>
        [JsonPropertyName("cmcLink")]
        public string? CmcLink { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal Weight { get; set; }
        /// <summary>
        /// Max precision
        /// </summary>
        [JsonPropertyName("maxPrecision")]
        public int MaxPrecision { get; set; }
        /// <summary>
        /// Deposit available
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public bool DepositStatus { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public bool WithdrawStatus { get; set; }
        /// <summary>
        /// Convert enabled
        /// </summary>
        [JsonPropertyName("convertEnabled")]
        public bool ConvertEnabled { get; set; }
        /// <summary>
        /// Transfer enabled
        /// </summary>
        [JsonPropertyName("transferEnabled")]
        public bool TransferEnabled { get; set; }
    }


}
