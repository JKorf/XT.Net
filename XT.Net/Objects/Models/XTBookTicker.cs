using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Book ticker
    /// </summary>
    [SerializationModel]
    public record XTBookTicker
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>aq</c>"] Best ask quantity
        /// </summary>
        [JsonPropertyName("aq")]
        public decimal? BestAskQuantity { get; set; }
        /// <summary>
        /// ["<c>bp</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bp")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>bq</c>"] Best bid quantity
        /// </summary>
        [JsonPropertyName("bq")]
        public decimal? BestBidQuantity { get; set; }
    }


}
