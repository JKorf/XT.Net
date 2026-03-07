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
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bc</c>"] Base asset
        /// </summary>
        [JsonPropertyName("bc")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>qc</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime? Timestamp { get; set; }
        /// <summary>
        /// ["<c>ct</c>"] Creation time
        /// </summary>
        [JsonPropertyName("ct")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// ["<c>i</c>"] Order id
        /// </summary>
        [JsonPropertyName("i")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>ci</c>"] Client order id
        /// </summary>
        [JsonPropertyName("ci")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>st</c>"] Order status
        /// </summary>
        [JsonPropertyName("st")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// ["<c>sd</c>"] Order side
        /// </summary>
        [JsonPropertyName("sd")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>tp</c>"] Order type
        /// </summary>
        [JsonPropertyName("tp")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>oq</c>"] Original quantity in base asset
        /// </summary>
        [JsonPropertyName("oq")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>oqq</c>"] Original quantity in quote asset
        /// </summary>
        [JsonPropertyName("oqq")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>eq</c>"] Quantity executed
        /// </summary>
        [JsonPropertyName("eq")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>lq</c>"] Quantity remaining
        /// </summary>
        [JsonPropertyName("lq")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Order price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Average fill price
        /// </summary>
        [JsonPropertyName("ap")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>f</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Fee { get; set; }
    }


}
