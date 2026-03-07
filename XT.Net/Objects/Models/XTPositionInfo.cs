using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record XTPositionInfo
    {
        /// <summary>
        /// ["<c>autoMargin</c>"] Whether to automatically call margin
        /// </summary>
        [JsonPropertyName("autoMargin")]
        public bool AutoMargin { get; set; }
        /// <summary>
        /// ["<c>availableCloseSize</c>"] Available close quantity
        /// </summary>
        [JsonPropertyName("availableCloseSize")]
        public decimal AvailableCloseQuantity { get; set; }
        /// <summary>
        /// ["<c>closeOrderSize</c>"] Close order quantity
        /// </summary>
        [JsonPropertyName("closeOrderSize")]
        public decimal CloseOrderQuantity { get; set; }
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
        /// ["<c>openOrderMarginFrozen</c>"] Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
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
        /// ["<c>realizedProfit</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedProfit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
