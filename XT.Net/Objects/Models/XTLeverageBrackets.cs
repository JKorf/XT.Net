using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Leverage brackets
    /// </summary>
    [SerializationModel]
    public record XTLeverageBrackets
    {
        /// <summary>
        /// ["<c>leverageBrackets</c>"] Leverage brackets
        /// </summary>
        [JsonPropertyName("leverageBrackets")]
        public XTLeverageBracket[] LeverageBrackets { get; set; } = Array.Empty<XTLeverageBracket>();
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Leverage bracket
    /// </summary>
    [SerializationModel]
    public record XTLeverageBracket
    {
        /// <summary>
        /// ["<c>bracket</c>"] Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }
        /// <summary>
        /// ["<c>maintMarginRate</c>"] Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// ["<c>maxLeverage</c>"] Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// ["<c>maxNominalValue</c>"] Max nominal value
        /// </summary>
        [JsonPropertyName("maxNominalValue")]
        public decimal MaxNominalValue { get; set; }
        /// <summary>
        /// ["<c>maxStartMarginRate</c>"] Max initial margin rate
        /// </summary>
        [JsonPropertyName("maxStartMarginRate")]
        public decimal? MaxInitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>minLeverage</c>"] Min leverage
        /// </summary>
        [JsonPropertyName("minLeverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// ["<c>startMarginRate</c>"] Initial margin rate
        /// </summary>
        [JsonPropertyName("startMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
