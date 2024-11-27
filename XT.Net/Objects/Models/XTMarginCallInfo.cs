using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Margin call info
    /// </summary>
    public record XTMarginCallInfo
    {
        /// <summary>
        /// Margin call price. 0 means no margin call
        /// </summary>
        [JsonPropertyName("breakPrice")]
        public decimal BreakPrice { get; set; }
        /// <summary>
        /// Calculated mark price
        /// </summary>
        [JsonPropertyName("calMarkPrice")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ProductType ContractType { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal PositionQuantity { get; set; }
        /// <summary>
        /// Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
