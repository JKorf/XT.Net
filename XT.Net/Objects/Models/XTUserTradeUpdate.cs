using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// User trade update
    /// </summary>
    [SerializationModel]
    public record XTUserTradeUpdate
    {
        /// <summary>
        /// ["<c>s</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("s")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>i</c>"] Trade id
        /// </summary>
        [JsonPropertyName("i")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>t</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>oi</c>"] Trade order id
        /// </summary>
        [JsonPropertyName("oi")]
        public long OrderId { get; set; }
        /// <summary>
        /// ["<c>p</c>"] Price
        /// </summary>
        [JsonPropertyName("p")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>q</c>"] Quantity
        /// </summary>
        [JsonPropertyName("q")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Quote quantity
        /// </summary>
        [JsonPropertyName("v")]
        public decimal QuoteQuantity { get; set; }
        /// <summary>
        /// ["<c>b</c>"] Whether the buyer is the maker
        /// </summary>
        [JsonPropertyName("b")]
        public bool BuyerIsMaker { get; set; }
    }

}
