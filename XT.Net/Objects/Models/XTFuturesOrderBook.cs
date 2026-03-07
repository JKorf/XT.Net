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
        /// ["<c>s</c>"] Symbol
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Date timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Last update id
        /// </summary>
        [JsonPropertyName("u")]
        public long UpdateId { get; set; }

        /// <summary>
        /// ["<c>a</c>"] Ask entries
        /// </summary>
        [JsonPropertyName("a")]
        public XTOrderBookEntry[] Asks { get; set; } = [];

        /// <summary>
        /// ["<c>b</c>"] Bid entries
        /// </summary>
        [JsonPropertyName("b")]
        public XTOrderBookEntry[] Bids { get; set; } = [];
    } 


}
