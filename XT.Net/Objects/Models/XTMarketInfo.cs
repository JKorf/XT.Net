using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Market info
    /// </summary>
    [SerializationModel]
    public record XTMarketInfo
    {
        /// <summary>
        /// ["<c>a</c>"] 24 hour volume
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>bp</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bp")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] 24 hour high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] 24 hour low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>m</c>"] Mark price
        /// </summary>
        [JsonPropertyName("m")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Price 24h ago
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>r</c>"] 24h price change percentage
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? ChangePercentage { get; set; }
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
        /// ["<c>v</c>"] 24h turnover
        /// </summary>
        [JsonPropertyName("v")]
        public decimal? Turnover { get; set; }
    }


}
