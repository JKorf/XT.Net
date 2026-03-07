using CryptoExchange.Net.Converters.SystemTextJson;
using System;
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
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>orderId</c>"] Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>clientOrderId</c>"] Client order id
        /// </summary>
        [JsonPropertyName("clientOrderId")]
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// ["<c>baseCurrency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCurrency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>side</c>"] Order side
        /// </summary>
        [JsonPropertyName("side")]
        public OrderSide OrderSide { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Order type
        /// </summary>
        [JsonPropertyName("type")]
        public OrderType OrderType { get; set; }
        /// <summary>
        /// ["<c>timeInForce</c>"] Time in force
        /// </summary>
        [JsonPropertyName("timeInForce")]
        public TimeInForce TimeInForce { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal? Price { get; set; }
        /// <summary>
        /// ["<c>origQty</c>"] Original quantity
        /// </summary>
        [JsonPropertyName("origQty")]
        public decimal? Quantity { get; set; }
        /// <summary>
        /// ["<c>origQuoteQty</c>"] Original quote quantity
        /// </summary>
        [JsonPropertyName("origQuoteQty")]
        public decimal? QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>executedQty</c>"] Quantity filled, in the placement asset
        /// </summary>
        [JsonPropertyName("executedQty")]
        public decimal? QuantityFilledPlacement { get; set; }
        /// <summary>
        /// ["<c>leavingQty</c>"] Quantity remaining to be filled, in the placement asset
        /// </summary>
        [JsonPropertyName("leavingQty")]
        public decimal? QuantityRemaining { get; set; }
        /// <summary>
        /// ["<c>tradeBase</c>"] Quantity filled
        /// </summary>
        [JsonPropertyName("tradeBase")]
        public decimal? QuantityFilled { get; set; }
        /// <summary>
        /// ["<c>tradeQuote</c>"] Value filled
        /// </summary>
        [JsonPropertyName("tradeQuote")]
        public decimal? QuoteQuantityFilled { get; set; }
        /// <summary>
        /// ["<c>avgPrice</c>"] Average price
        /// </summary>
        [JsonPropertyName("avgPrice")]
        public decimal? AveragePrice { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee paid
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal? Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string? FeeAsset { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status of the order
        /// </summary>
        [JsonPropertyName("state")]
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// ["<c>deductServices</c>"] Fee deduction list (if set XT deduction fee and the deduction occurs, use this field to represent the trade fee. Otherwise, use the original fee and feeCurrency fields to represent the trade fee). 
        /// </summary>
        [JsonPropertyName("deductServices")]
        public XTOrderFee[] Fees { get; set; } = Array.Empty<XTOrderFee>();
        /// <summary>
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>ip</c>"] Ip
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>updatedTime</c>"] Updated time
        /// </summary>
        [JsonPropertyName("updatedTime")]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// ["<c>closed</c>"] Closed
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
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>feeCurrency</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("feeCurrency")]
        public string FeeAsset { get; set; } = string.Empty;
    }


}
