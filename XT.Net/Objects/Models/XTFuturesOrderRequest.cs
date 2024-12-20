using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order request
    /// </summary>
    public record XTFuturesOrderRequest
    {
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
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
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Order limit price
        /// </summary>
        [JsonPropertyName("price"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? Price { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? TriggerProfitPrice { get; set; }
        /// <summary>
        /// Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? TriggerStopPrice { get; set; }

        [JsonInclude, JsonPropertyName("media")]
        internal string Media => XTExchange.ClientRef;
    }
}
