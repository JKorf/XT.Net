using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Data page
    /// </summary>
    public record XTDataPage<T>
    {
        /// <summary>
        /// ["<c>page</c>"] Page
        /// </summary>
        [JsonPropertyName("page")]
        public int? Page { get; set; }
        /// <summary>
        /// ["<c>ps</c>"] Page size
        /// </summary>
        [JsonPropertyName("ps")]
        public int? PageSize { get; set; }
        /// <summary>
        /// ["<c>total</c>"] Total items
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// ["<c>items</c>"] Items
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Data { get; set; } = [];
    }
}
