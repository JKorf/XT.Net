using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Balance
    /// </summary>
    [SerializationModel]
    public record XTBalanceUpdate
    {
        /// <summary>
        /// ["<c>a</c>"] Account id
        /// </summary>
        [JsonPropertyName("a")]
        public long AccountId { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>c</c>"] Asset name
        /// </summary>
        [JsonPropertyName("c")]
        public string Asset { get; set; } = string.Empty;

        /// <summary>
        /// ["<c>f</c>"] Frozen quantity
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Total quantity
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Total { get; set; }
        /// <summary>
        /// ["<c>z</c>"] Business type
        /// </summary>
        [JsonPropertyName("z")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string? Symbol { get; set; }
    }


}
