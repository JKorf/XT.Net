using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record XTBaseOrderBookUpdate
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
    }

    /// <summary>
    /// Order book update
    /// </summary>
    [SerializationModel]
    public record XTOrderBookUpdate : XTBaseOrderBookUpdate
    {
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record XTIncrementalOrderBookUpdate : XTOrderBookUpdate
    {
        /// <summary>
        /// ["<c>fi</c>"] First update id
        /// </summary>
        [JsonPropertyName("fi")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Last update id
        /// </summary>
        [JsonPropertyName("i")]
        public long LastUpdateId { get; set; }
    }
}
