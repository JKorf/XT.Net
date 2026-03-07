using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Notification update
    /// </summary>
    [SerializationModel]
    public record XTNotification
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>positionType</c>"] Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionSize</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>notifyType</c>"] Notify type
        /// </summary>
        [JsonPropertyName("notifyType")]
        public NotificationType NotifyType { get; set; }
    }


}
