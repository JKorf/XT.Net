using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
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
        public XTOrderBookEntry[] Asks { get; set; } = [];

        /// <summary>
        /// Bid entries
        /// </summary>
        [JsonPropertyName("b")]
        public XTOrderBookEntry[] Bids { get; set; } = [];
    } 


}
