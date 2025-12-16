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
        /// Account id
        /// </summary>
        [JsonPropertyName("accountId")]
        public long AccountId { get; set; }
        /// <summary>
        /// Allow open position
        /// </summary>
        [JsonPropertyName("allowOpenPosition")]
        public bool AllowOpenPosition { get; set; }
        /// <summary>
        /// Allow trade
        /// </summary>
        [JsonPropertyName("allowTrade")]
        public bool AllowTrade { get; set; }
        /// <summary>
        /// Allow transfer
        /// </summary>
        [JsonPropertyName("allowTransfer")]
        public bool AllowTransfer { get; set; }
        /// <summary>
        /// Open time
        /// </summary>
        [JsonPropertyName("openTime")]
        public DateTime OpenTime { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public AccountStatus Status { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("userId")]
        public long UserId { get; set; }
    }


}
