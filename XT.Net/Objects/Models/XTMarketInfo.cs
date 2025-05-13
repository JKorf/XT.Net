using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// 24 hour volume
        /// </summary>
        [JsonPropertyName("a")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? BestAskPrice { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bp")]
        public decimal? BestBidPrice { get; set; }
        /// <summary>
        /// Last trade price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// 24 hour high price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("i")]
        public decimal? IndexPrice { get; set; }
        /// <summary>
        /// 24 hour low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Mark price
        /// </summary>
        [JsonPropertyName("m")]
        public decimal? MarkPrice { get; set; }
        /// <summary>
        /// Price 24h ago
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// 24h price change percentage
        /// </summary>
        [JsonPropertyName("r")]
        public decimal? ChangePercentage { get; set; }
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
        /// 24h turnover
        /// </summary>
        [JsonPropertyName("v")]
        public decimal? Turnover { get; set; }
    }


}
