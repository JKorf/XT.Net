using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Margin call info
    /// </summary>
    [SerializationModel]
    public record XTMarginCallInfo
    {
        /// <summary>
        /// ["<c>breakPrice</c>"] Margin call price. 0 means no margin call
        /// </summary>
        [JsonPropertyName("breakPrice")]
        public decimal BreakPrice { get; set; }
        /// <summary>
        /// ["<c>calMarkPrice</c>"] Calculated mark price
        /// </summary>
        [JsonPropertyName("calMarkPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ProductType ContractType { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
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
        /// ["<c>positionType</c>"] Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
