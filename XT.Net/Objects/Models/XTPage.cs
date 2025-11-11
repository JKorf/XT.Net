using CryptoExchange.Net.Converters.SystemTextJson;
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
    [SerializationModel]
    public record XTPage<T>
    {
        /// <summary>
        /// Whether there is a next page
        /// </summary>
        [JsonPropertyName("hasNext")]
        public bool? HasNext { get; set; }
        /// <summary>
        /// Whether there is a previous page
        /// </summary>
        [JsonPropertyName("hasPrev")]
        public bool? HasPrevious { get; set; }

        private T[]? _data;
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Data
        {
            get => _data ?? [];
            set => _data = value;
        }
    }
}
