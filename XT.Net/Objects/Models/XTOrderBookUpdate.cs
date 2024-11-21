using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record XTBaseOrderBookUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
    
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("a")]
        public IEnumerable<XTOrderBookEntry> Bids { get; set; } = Array.Empty<XTOrderBookEntry>();
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("b")]
        public IEnumerable<XTOrderBookEntry> Asks { get; set; } = Array.Empty<XTOrderBookEntry>();
    }

    /// <summary>
    /// Order book update
    /// </summary>
    public record XTOrderBookUpdate : XTBaseOrderBookUpdate
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Order book info
    /// </summary>
    public record XTIncrementalOrderBookUpdate : XTBaseOrderBookUpdate
    {
        /// <summary>
        /// First update id
        /// </summary>
        [JsonPropertyName("fi")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("i")]
        public long LastUpdateId { get; set; }
    }
}
