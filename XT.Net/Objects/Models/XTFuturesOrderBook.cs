using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    public record XTFuturesOrderBook
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Date timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }

        /// <summary>
        /// Ask entries
        /// </summary>
        [JsonPropertyName("a")]
        public IEnumerable<XTOrderBookEntry> Asks { get; set; } = [];

        /// <summary>
        /// Bid entries
        /// </summary>
        [JsonPropertyName("b")]
        public IEnumerable<XTOrderBookEntry> Bids { get; set; } = [];
    } 


}
