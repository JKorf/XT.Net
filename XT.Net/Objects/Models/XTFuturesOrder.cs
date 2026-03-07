using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Futures order info
    /// </summary>
    [SerializationModel]
    public record XTFuturesOrder
    {
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal AveragePrice { get; set; }
        /// <summary>
        /// ["<c>closePosition</c>"] Close position flag when triggered
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool ClosePosition { get; set; }
        /// <summary>
        /// ["<c>closeProfit</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("closeProfit")]
        public decimal? RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Created time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>forceClose</c>"] Is liquidation order
        /// </summary>
        [JsonPropertyName("forceClose")]
        public bool Liquidation { get; set; }
        /// <summary>
        /// ["<c>marginFrozen</c>"] Margin frozen
        /// </summary>
        [JsonPropertyName("marginFrozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>orderType</c>"] Order type
        /// </summary>
        [JsonPropertyName("orderType")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] Order quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Limit price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>sourceId</c>"] Triggering conditions ID
        /// </summary>
        [JsonPropertyName("sourceId")]
        public long? SourceId { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Order status
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>triggerProfitPrice</c>"] Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal? TriggerProfitPrice { get; set; }
        /// <summary>
        /// ["<c>triggerStopPrice</c>"] Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal? TriggerStopPrice { get; set; }
        /// <summary>
        /// ["<c>sourceType</c>"] Source type
        /// </summary>
        [JsonPropertyName("sourceType")]
        public SourceType? SourceType { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal? Leverage { get; set; }
        /// <summary>
        /// ["<c>positionType</c>"] Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType? PositionType { get; set; }
    }


}
