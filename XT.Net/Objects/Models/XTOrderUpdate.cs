using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record XTOrderUpdate
    {
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("bc")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonPropertyName("ct")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("ci")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Order status
        /// </summary>
        [JsonPropertyName("st")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("sd")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("tp")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Original quantity in base asset
        /// </summary>
        [JsonPropertyName("oq")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Original quantity in quote asset
        /// </summary>
        [JsonPropertyName("oqq")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Quantity executed
        /// </summary>
        [JsonPropertyName("eq")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Quantity remaining
        /// </summary>
        [JsonPropertyName("lq")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Average fill price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Fee { get; set; }
    }


}
