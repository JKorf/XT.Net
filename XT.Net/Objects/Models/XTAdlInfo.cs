using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Auto deleverage info
    /// </summary>
    [SerializationModel]
    public record XTAdlInfo
    {
        /// <summary>
        /// ["<c>longQuantile</c>"] Long quantile
        /// </summary>
        [JsonPropertyName("longQuantile")]
        public decimal LongQuantile { get; set; }
        /// <summary>
        /// ["<c>shortQuantile</c>"] Short quantile
        /// </summary>
        [JsonPropertyName("shortQuantile")]
        public decimal ShortQuantile { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
