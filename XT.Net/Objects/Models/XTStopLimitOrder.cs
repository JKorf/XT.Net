using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>createdTime</c>"] Created time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal? EntryPrice { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal? IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] Quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionSize</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal? PositionQuantity { get; set; }
        /// <summary>
        /// ["<c>profitId</c>"] Order id
        /// </summary>
        [JsonPropertyName("profitId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Trigger order stauts
        /// </summary>
        [JsonPropertyName("state")]
        public TriggerOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>triggerProfitPrice</c>"] Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal TriggerProfitPrice { get; set; }
        /// <summary>
        /// ["<c>triggerStopPrice</c>"] Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal TriggerStopPrice { get; set; }
        /// <summary>
        /// ["<c>positionType</c>"] Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
    }


}
