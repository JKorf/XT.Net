using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Account info
    /// </summary>
    [SerializationModel]
    public record XTFuturesAccountInfo
    {
        /// <summary>
        /// ["<c>accountId</c>"] Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>allowOpenPosition</c>"] Allow open position
        /// </summary>
        [JsonPropertyName("allowOpenPosition")]
        public bool AllowOpenPosition { get; set; }
        /// <summary>
        /// ["<c>allowTrade</c>"] Allow trade
        /// </summary>
        [JsonPropertyName("allowTrade")]
        public bool AllowTrade { get; set; }
        /// <summary>
        /// ["<c>allowTransfer</c>"] Allow transfer
        /// </summary>
        [JsonPropertyName("allowTransfer")]
        public bool AllowTransfer { get; set; }
        /// <summary>
        /// ["<c>openTime</c>"] Open time
        /// </summary>
        [JsonPropertyName("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public AccountStatus Status { get; set; }
        /// <summary>
        /// ["<c>userId</c>"] User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
    }


}
