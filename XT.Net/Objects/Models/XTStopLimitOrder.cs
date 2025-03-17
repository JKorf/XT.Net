using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Stop limit order
    /// </summary>
    [SerializationModel]
    public record XTStopLimitOrder
    {
        /// <summary>
        /// Created time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal? IsolatedMargin { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal? PositionQuantity { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("profitId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Trigger order stauts
        /// </summary>
        [JsonPropertyName("state")]
        public TriggerOrderStatus Status { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal TriggerProfitPrice { get; set; }
        /// <summary>
        /// Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal TriggerStopPrice { get; set; }
        /// <summary>
        /// Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
    }


}
