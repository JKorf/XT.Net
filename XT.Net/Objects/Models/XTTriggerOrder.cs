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
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>updatedTime</c>"] Update time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
    }


}
