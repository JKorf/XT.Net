using CryptoExchange.Net.Converters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order book info
    /// </summary>
    [SerializationModel]
    public record XTOrderBook
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Last update id
        /// </summary>
        [JsonPropertyName("lastUpdateId")]
        public long LastUpdateId { get; set; }
    
        /// <summary>
        /// Bids
        /// </summary>
        [JsonPropertyName("bids")]
        public XTOrderBookEntry[] Bids { get; set; } = Array.Empty<XTOrderBookEntry>();
        /// <summary>
        /// Asks
        /// </summary>
        [JsonPropertyName("asks")]
        public XTOrderBookEntry[] Asks { get; set; } = Array.Empty<XTOrderBookEntry>();
    }

    /// <summary>
    /// Order book entry
    /// </summary>
    [JsonConverter(typeof(ArrayConverter<XTOrderBookEntry>))]
    [SerializationModel]
    public record XTOrderBookEntry : ISymbolOrderBookEntry
    {
        /// <summary>
        /// Price
        /// </summary>
        [ArrayProperty(0)]
        public decimal Price { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [ArrayProperty(1)]
        public decimal Quantity { get; set; }
    }
}
