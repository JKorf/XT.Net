using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Open interest
    /// </summary>
    public record XTOpenInterest
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("openInterest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Open interest usd
        /// </summary>
        [JsonPropertyName("openInterestUsd")]
        public decimal OpenInterestUsd { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Timestamp { get; set; }
    }


}
