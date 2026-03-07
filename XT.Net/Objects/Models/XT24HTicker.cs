using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// 24 hour price stats ticker
    /// </summary>
    [SerializationModel]
    public record XT24HTicker
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
        /// ["<c>cv</c>"] 24 hour change
        /// </summary>
        [JsonPropertyName("cv")]
        public decimal? Change { get; set; }
        /// <summary>
        /// ["<c>cr</c>"] 24 hour change percentage
        /// </summary>
        [JsonPropertyName("cr")]
        public decimal? ChangePercentage { get; set; }
        /// <summary>
        /// ["<c>o</c>"] Open price
        /// </summary>
        [JsonPropertyName("o")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// ["<c>l</c>"] Low price
        /// </summary>
        [JsonPropertyName("l")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>h</c>"] High price
        /// </summary>
        [JsonPropertyName("h")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Last price
        /// </summary>
        [JsonPropertyName("c")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Volume in base asset
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Volume in quote asset
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteVolume { get; set; }
    }


}
