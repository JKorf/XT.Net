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
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version of the data
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currencies</c>"] Assets
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fullName</c>"] Full name
        /// </summary>
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>logo</c>"] Logo
        /// </summary>
        [JsonPropertyName("logo")]
        public string? Logo { get; set; }
        /// <summary>
        /// ["<c>cmcLink</c>"] Cmc link
        /// </summary>
        [JsonPropertyName("cmcLink")]
        public string? CmcLink { get; set; }
        /// <summary>
        /// ["<c>weight</c>"] Weight
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal Weight { get; set; }
        /// <summary>
        /// ["<c>maxPrecision</c>"] Max precision
        /// </summary>
        [JsonPropertyName("maxPrecision")]
        public int MaxPrecision { get; set; }
        /// <summary>
        /// ["<c>depositStatus</c>"] Deposit available
        /// </summary>
        [JsonPropertyName("depositStatus")]
        public bool DepositStatus { get; set; }
        /// <summary>
        /// ["<c>withdrawStatus</c>"] Withdraw available
        /// </summary>
        [JsonPropertyName("withdrawStatus")]
        public bool WithdrawStatus { get; set; }
        /// <summary>
        /// ["<c>convertEnabled</c>"] Convert enabled
        /// </summary>
        [JsonPropertyName("convertEnabled")]
        public bool ConvertEnabled { get; set; }
        /// <summary>
        /// ["<c>transferEnabled</c>"] Transfer enabled
        /// </summary>
        [JsonPropertyName("transferEnabled")]
        public bool TransferEnabled { get; set; }
    }


}
