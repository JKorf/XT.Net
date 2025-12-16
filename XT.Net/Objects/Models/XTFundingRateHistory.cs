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
        /// Funding interval in seconds
        /// </summary>
        [JsonPropertyName("collectionInternal")]
        public int FundingInterval { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
