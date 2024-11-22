using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Ticker info
    /// </summary>
    public record XTFuturesTicker
    {
        /// <summary>
        /// Volume
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal LastPrice { get; set; }
        /// <summary>
        /// Highest price last 24h
        /// </summary>
        [JsonPropertyName("h")]
        public decimal HighPrice { get; set; }
        /// <summary>
        /// Lowest price last 24h
        /// </summary>
        [JsonPropertyName("l")]
        public decimal LowPrice { get; set; }
        /// <summary>
        /// Price 24h ago
        /// </summary>
        [JsonPropertyName("o")]
        public decimal OpenPrice { get; set; }
        /// <summary>
        /// Price change since 24h ago
        /// </summary>
        [JsonPropertyName("r")]
        public decimal PriceChange { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Turnover in last 24h
        /// </summary>
        [JsonPropertyName("v")]
        public decimal Turnover { get; set; }
    }


}
