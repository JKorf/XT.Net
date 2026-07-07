using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Trigger order info
    /// </summary>
    [SerializationModel]
    public record XTTriggerOrder
    {
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>closePosition</c>"] Whether triggered to close all
        /// </summary>
        [JsonPropertyName("closePosition")]
        public bool? ClosePosition { get; set; }
        /// <summary>
        /// ["<c>createdTime</c>"] Created time
        /// </summary>
        [JsonPropertyName("createdTime")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// ["<c>entrustId</c>"] Order id
        /// </summary>
        [JsonPropertyName("entrustId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>entrustType</c>"] Order type
        /// </summary>
        [JsonPropertyName("entrustType")]
        public TriggerOrderType TriggerOrderType { get; set; }
        /// <summary>
        /// ["<c>marketOrderLevel</c>"] Market order level
        /// </summary>
        [JsonPropertyName("marketOrderLevel")]
        public decimal? MarketOrderLevel { get; set; }
        /// <summary>
        /// ["<c>orderSide</c>"] Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>ordinary</c>"] Ordinary
        /// </summary>
        [JsonPropertyName("ordinary")]
        public bool Ordinary { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Order price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Trigger order status
        /// </summary>
        [JsonPropertyName("state")]
        public TriggerOrderStatus Status { get; set; }
        /// <summary>
        /// ["<c>stopPrice</c>"] Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce? TimeInForce { get; set; }
        /// <summary>
        /// ["<c>triggerPriceType</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public PriceType TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>delegateTriggerPriceType</c>"] Delegate trigger price type
        /// </summary>
        [JsonPropertyName("delegateTriggerPriceType")]
        public PriceType? DelegateTriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>profitDelegateOrderType</c>"] Delegate profit order type
        /// </summary>
        [JsonPropertyName("profitDelegateOrderType")]
        public TriggerOrderType? ProfitDelegateOrderType { get; set; }
        /// <summary>
        /// ["<c>stopDelegateOrderType</c>"] Delegate stop order type
        /// </summary>
        [JsonPropertyName("stopDelegateOrderType")]
        public TriggerOrderType? StopDelegateOrderType { get; set; }
        /// <summary>
        /// ["<c>profitDelegateTimeInForce</c>"] Profit delegate time in force
        /// </summary>
        [JsonPropertyName("profitDelegateTimeInForce")]
        public TimeInForce? ProfitDelegateTimeInForce { get; set; }
        /// <summary>
        /// ["<c>profitDelegatePrice</c>"] Profit delegate price
        /// </summary>
        [JsonPropertyName("profitDelegatePrice")]
        public decimal? ProfitDelegatePrice { get; set; }
        /// <summary>
        /// ["<c>stopDelegateTimeInForce</c>"] Stop delegate time in force
        /// </summary>
        [JsonPropertyName("stopDelegateTimeInForce")]
        public TimeInForce? StopDelegateTimeInForce { get; set; }
        /// <summary>
        /// ["<c>stopDelegatePrice</c>"] Stop delegate price
        /// </summary>
        [JsonPropertyName("stopDelegatePrice")]
        public decimal? StopDelegatePrice { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilled { get; set; }
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
        /// ["<c>reverseOpenExecutedQty</c>"] Reverse open executed quantity
        /// </summary>
        [JsonPropertyName("reverseOpenExecutedQty")]
        public decimal? ReverseOpenExecutedQuantity { get; set; }
        /// <summary>
        /// ["<c>reverseOpenAvgPrice</c>"] Reverse open average price
        /// </summary>
        [JsonPropertyName("reverseOpenAvgPrice")]
        public decimal? ReverseOpenAveragePrice { get; set; }
        /// <summary>
        /// ["<c>reverseOrderId</c>"] Reverse order id
        /// </summary>
        [JsonPropertyName("reverseOrderId")]
        public long? ReverseOrderId { get; set; }
        /// <summary>
        /// ["<c>reverseOpenOrderId</c>"] Reverse open order id
        /// </summary>
        [JsonPropertyName("reverseOpenOrderId")]
        public long? ReverseOpenOrderId { get; set; }
        /// <summary>
        /// ["<c>desc</c>"] Description
        /// </summary>
        [JsonPropertyName("desc")]
        public string? Description { get; set; }
    }


}
