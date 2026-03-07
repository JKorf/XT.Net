using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record XTBaseFuturesOrderBookUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
    
        /// <summary>
        /// ["<c>b</c>"] Bids
        /// </summary>
        [JsonPropertyName("b")]
        public XTOrderBookEntry[] Bids { get; set; } = Array.Empty<XTOrderBookEntry>();
        /// <summary>
        /// ["<c>a</c>"] Asks
        /// </summary>
        [JsonPropertyName("a")]
        public XTOrderBookEntry[] Asks { get; set; } = Array.Empty<XTOrderBookEntry>();

        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record XTFuturesOrderBookUpdate : XTBaseFuturesOrderBookUpdate
    {
    }

    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record XTFuturesIncrementalOrderBookUpdate : XTBaseFuturesOrderBookUpdate
    {
        /// <summary>
        /// ["<c>fu</c>"] First update id
        /// </summary>
        [JsonPropertyName("fu")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// ["<c>pu</c>"] Previous update id
        /// </summary>
        [JsonPropertyName("pu")]
        public long PreviousUpdateId { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Last update id
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }
    }
}
