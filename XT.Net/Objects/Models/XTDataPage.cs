using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Data page
    /// </summary>
    public class XTDataPage<T>
    {
        /// <summary>
        /// Page
        /// </summary>
        [JsonPropertyName("page")]
        public int? Page { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        [JsonPropertyName("ps")]
        public int? PageSize { get; set; }
        /// <summary>
        /// Total items
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public T[] Data { get; set; } = [];
    }
}
