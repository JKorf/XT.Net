using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Funding rate
    /// </summary>
    [SerializationModel]
    public record XTFundingRateHistory
    {
        /// <summary>
        /// ["<c>collectionInternal</c>"] Funding interval in seconds
        /// </summary>
        [JsonPropertyName("collectionInternal")]
        public int FundingInterval { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>fundingRate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
