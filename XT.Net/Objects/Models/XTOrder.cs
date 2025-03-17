using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Order info
    /// </summary>
    [SerializationModel]
    public record XTOrder
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// Original quote quantity
        /// </summary>
        [JsonPropertyName("origQuoteQty")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// Quantity filled, in the placement asset
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal QuantityFilledPlacement { get; set; }
        /// <summary>
        /// Quantity remaining to be filled, in the placement asset
        /// </summary>
        [JsonPropertyName("leavingQty")]
        public decimal QuantityRemaining { get; set; }
        /// <summary>
        /// Quantity filled
        /// </summary>
        [JsonPropertyName("tradeBase")]
        public decimal QuantityFilled { get; set; }
        /// <summary>
        /// Value filled
        /// </summary>
        [JsonPropertyName("tradeQuote")]
        public decimal QuoteQuantityFilled { get; set; }
        /// <summary>
        /// Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// Status of the order
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Fee deduction list (if set XT deduction fee and the deduction occurs, use this field to represent the trade fee. Otherwise, use the original fee and feeCurrency fields to represent the trade fee). 
        /// </summary>
        [JsonPropertyName("deductServices")]
        public XTOrderFee[] Fees { get; set; } = Array.Empty<XTOrderFee>();
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Ip
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
        /// <summary>
        /// Updated time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Closed
        /// </summary>
        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
    }

    /// <summary>
    /// Fee info
    /// </summary>
    [SerializationModel]
    public record XTOrderFee
    {
        /// <summary>
        /// Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
    }


}
