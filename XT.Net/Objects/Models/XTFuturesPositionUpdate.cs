using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Position update
    /// </summary>
    [SerializationModel]
    public record XTFuturesPositionUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ProductType ContractType { get; set; }
        /// <summary>
        /// Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// Close order quantity
        /// </summary>
        [JsonPropertyName("closeOrderSize")]
        public decimal CloseOrderQuantity { get; set; }
        /// <summary>
        /// Available close quantity
        /// </summary>
        [JsonPropertyName("availableCloseSize")]
        public decimal AvailableCloseQuantity { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedProfit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
    }


}
