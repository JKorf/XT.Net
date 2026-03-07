using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Funding fee
    /// </summary>
    [SerializationModel]
    public record XTFundingRate
    {
        /// <summary>
        /// ["<c>collectionInternal</c>"] Funding rate interval in hours
        /// </summary>
        [JsonPropertyName("collectionInternal")]
        public int? FundingRateInterval { get; set; }
        /// <summary>
        /// ["<c>nextCollectionTime</c>"] Next funding time
        /// </summary>
        [JsonPropertyName("nextCollectionTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>fundingRate</c>"] Latest funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal LastFundingRate { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
