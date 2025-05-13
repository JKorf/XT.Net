using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Activation price
        /// </summary>
        [JsonPropertyName("activationPrice")]
        public decimal? ActivationPrice { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Callback track config
        /// </summary>
        [JsonPropertyName("callback")]
        public TrackRange TrackType { get; set; }
        /// <summary>
        /// Callback value
        /// </summary>
        [JsonPropertyName("callbackVal")]
        public decimal CallbackValue { get; set; }
        /// <summary>
        /// Whether to configure activation price
        /// </summary>
        [JsonPropertyName("configActivation")]
        public bool ConfigActivation { get; set; }
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
        /// Current price
        /// </summary>
        [JsonPropertyName("currentPrice")]
        public decimal CurrentPrice { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        [JsonPropertyName("desc")]
        public string? Description { get; set; }
        /// <summary>
        /// Executed quantity
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("orderSide")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Ordinary
        /// </summary>
        [JsonPropertyName("ordinary")]
        public bool Ordinary { get; set; }
        /// <summary>
        /// Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Track order status
        /// </summary>
        [JsonPropertyName("state")]
        public TrackOrderStatus TrackOrderStatus { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        [JsonPropertyName("stopPrice")]
        public decimal StopPrice { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Track id
        /// </summary>
        [JsonPropertyName("trackId")]
        public long TrackId { get; set; }
        /// <summary>
        /// Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public PriceType TriggerPriceType { get; set; }
    }


}
