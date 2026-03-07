using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record XTTrackOrder
    {
        /// <summary>
        /// ["<c>activationPrice</c>"] Activation price
        /// </summary>
        [JsonPropertyName("activationPrice")]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>callback</c>"] Callback track config
        /// </summary>
        [JsonPropertyName("callback")]
        public TrackRange TrackType { get; set; }
        /// <summary>
        /// ["<c>callbackVal</c>"] Callback value
        /// </summary>
        [JsonPropertyName("callbackVal")]
        public decimal CallbackValue { get; set; }
        /// <summary>
        /// ["<c>configActivation</c>"] Whether to configure activation price
        /// </summary>
        [JsonPropertyName("configActivation")]
        public bool ConfigActivation { get; set; }
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
        /// ["<c>currentPrice</c>"] Current price
        /// </summary>
        [JsonPropertyName("currentPrice")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// ["<c>desc</c>"] Description
        /// </summary>
        [JsonPropertyName("desc")]
        public string? Description { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilled { get; set; }
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
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Track order status
        /// </summary>
        [JsonPropertyName("state")]
        public TrackOrderStatus TrackOrderStatus { get; set; }
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
        /// ["<c>trackId</c>"] Track id
        /// </summary>
        [JsonPropertyName("trackId")]
        public long TrackId { get; set; }
        /// <summary>
        /// ["<c>triggerPriceType</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public PriceType TriggerPriceType { get; set; }
    }


}
