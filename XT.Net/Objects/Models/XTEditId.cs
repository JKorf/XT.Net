using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Edit id
    /// </summary>
    [SerializationModel]
    public record XTEditId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Modify id
        /// </summary>
        [JsonPropertyName("modifyId")]
        public long ModifyId { get; set; }
    }
}
