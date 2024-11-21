using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// 24 hour price stats ticker
    /// </summary>
    public record XT24HTicker
    {
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
        /// 24 hour change
        /// </summary>
        [JsonPropertyName("cv")]
        public decimal? Change { get; set; }
        /// <summary>
        /// 24 hour change percentage
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// Volume in base asset
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Volume in quote asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteVolume { get; set; }
    }


}
