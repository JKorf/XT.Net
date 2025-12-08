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
        /// Funding rate interval in hours
        /// </summary>
        [JsonPropertyName("collectionInternal")]
        public int? FundingRateInterval { get; set; }
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonPropertyName("nextCollectionTime")]
        public DateTime NextFundingTime { get; set; }
        /// <summary>
        /// Latest funding rate
        /// </summary>
        [JsonPropertyName("fundingRate")]
        public decimal LastFundingRate { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
