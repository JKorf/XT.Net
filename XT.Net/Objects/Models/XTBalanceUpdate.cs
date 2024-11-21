using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Balance
    /// </summary>
    public record XTBalanceUpdate
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonPropertyName("currency")]
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("t")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("c")]
        public string Asset { get; set; } = string.Empty;
        
        /// <summary>
        /// Frozen quantity
        /// </summary>
        [JsonPropertyName("f")]
        public decimal Frozen { get; set; }
        /// <summary>
        /// Total quantity
        /// </summary>
        [JsonPropertyName("b")]
        public decimal Total { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("z")]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("x")]
        public string? Symbol { get; set; }
    }


}
