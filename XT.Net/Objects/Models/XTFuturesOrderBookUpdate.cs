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
    public record XTBaseFuturesOrderBookUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
    
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("b")]
        public IEnumerable<XTOrderBookEntry> Bids { get; set; } = Array.Empty<XTOrderBookEntry>();
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("a")]
        public IEnumerable<XTOrderBookEntry> Asks { get; set; } = Array.Empty<XTOrderBookEntry>();

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
    }

    /// <summary>
    /// Order book update
    /// </summary>
    public record XTFuturesOrderBookUpdate : XTBaseFuturesOrderBookUpdate
    {
    }

    /// <summary>
    /// Order book info
    /// </summary>
    public record XTFuturesIncrementalOrderBookUpdate : XTBaseFuturesOrderBookUpdate
    {
        /// <summary>
        /// First update id
        /// </summary>
        [JsonPropertyName("fu")]
        public long FirstUpdateId { get; set; }
        /// <summary>
        /// Previous update id
        /// </summary>
        [JsonPropertyName("pu")]
        public long PreviousUpdateId { get; set; }
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("u")]
        public long LastUpdateId { get; set; }
    }
}
