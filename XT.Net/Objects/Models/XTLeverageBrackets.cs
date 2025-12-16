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
        /// Leverage brackets
        /// </summary>
        [JsonPropertyName("leverageBrackets")]
        public XTLeverageBracket[] LeverageBrackets { get; set; } = Array.Empty<XTLeverageBracket>();
        /// <summary>
        /// Symbol
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
        /// Bracket
        /// </summary>
        [JsonPropertyName("bracket")]
        public int Bracket { get; set; }
        /// <summary>
        /// Maintenance margin rate
        /// </summary>
        [JsonPropertyName("maintMarginRate")]
        public decimal MaintenanceMarginRate { get; set; }
        /// <summary>
        /// Max leverage
        /// </summary>
        [JsonPropertyName("maxLeverage")]
        public decimal MaxLeverage { get; set; }
        /// <summary>
        /// Max nominal value
        /// </summary>
        [JsonPropertyName("maxNominalValue")]
        public decimal MaxNominalValue { get; set; }
        /// <summary>
        /// Max initial margin rate
        /// </summary>
        [JsonPropertyName("maxStartMarginRate")]
        public decimal? MaxInitialMarginRate { get; set; }
        /// <summary>
        /// Min leverage
        /// </summary>
        [JsonPropertyName("minLeverage")]
        public decimal MinLeverage { get; set; }
        /// <summary>
        /// Initial margin rate
        /// </summary>
        [JsonPropertyName("startMarginRate")]
        public decimal InitialMarginRate { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }


}
