using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Futures order info
    /// </summary>
    public record XTFuturesOrder
    {
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// Close position flag when triggered
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("closeProfit")]
        public decimal? RealizedPnl { get; set; }
        /// <summary>
        /// Created time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Is liquidation order
        /// </summary>
        [JsonPropertyName("forceClose")]
        public bool Liquidation { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("marginFrozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Triggering conditions ID
        /// </summary>
        [JsonPropertyName("sourceId")]
        public long? SourceId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal? TriggerProfitPrice { get; set; }
        /// <summary>
        /// Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal? TriggerStopPrice { get; set; }
    }


}
