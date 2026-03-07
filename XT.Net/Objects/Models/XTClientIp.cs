using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Client IP
    /// </summary>
    [SerializationModel]
    public record XTClientIp
    {
        /// <summary>
        /// ["<c>ip</c>"] Ip
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
    }


}
