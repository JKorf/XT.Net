using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// ["<c>hasNext</c>"] Whether there is a next page
        /// </summary>
        [JsonPropertyName("hasNext")]
        public bool? HasNext { get; set; }
        /// <summary>
        /// ["<c>hasPrev</c>"] Whether there is a previous page
        /// </summary>
        [JsonPropertyName("hasPrev")]
        public bool? HasPrevious { get; set; }
        /// <summary>
        /// ["<c>page</c>"] Page
        /// </summary>
        [JsonPropertyName("page")]
        public int? Page { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total
        /// </summary>
        [JsonPropertyName("total")]
        public int? Total { get; set; }
        /// <summary>
        /// ["<c>ps</c>"] Page size
        /// </summary>
        [JsonPropertyName("ps")]
        public int? PageSize { get; set; }

        private T[]? _data;
        /// <summary>
        /// ["<c>items</c>"] Data
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Data
        {
            get => _data ?? [];
            set => _data = value;
        }
    }
}
