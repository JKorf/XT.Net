using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    [SerializationModel]
    public record XTFuturesTicker
    {
        /// <summary>
        /// ["<c>a</c>"] Volume
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] Highest price last 24h
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Lowest price last 24h
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Price 24h ago
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// ["<c>r</c>"] Price change since 24h ago
        /// </summary>
        [JsonPropertyName("r")]
        public decimal PriceChange { get; set; }
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
        /// ["<c>v</c>"] Turnover in last 24h
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Turnover { get; set; }
    }


}
