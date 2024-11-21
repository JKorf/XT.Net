using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Data page
    /// </summary>
    public record XTPage<T>
    {
        /// <summary>
        /// Whether there is a next page
        /// </summary>
        [JsonPropertyName("hasNext")]
        public bool HasNext { get; set; }
        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        [JsonPropertyName("hasPrev")]
        public bool HasPrevious { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("items")]
        public IEnumerable<T> Data { get; set; } = [];
    }
}
