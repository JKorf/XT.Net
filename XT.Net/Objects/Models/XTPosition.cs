using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Position info
    /// </summary>
    [SerializationModel]
    public record XTPosition
    {
        /// <summary>
        /// Whether to automatically call margin
        /// </summary>
        [JsonPropertyName("autoMargin")]
        public bool AutoMargin { get; set; }
        /// <summary>
        /// Available close quantity
        /// </summary>
        [JsonPropertyName("availableCloseSize")]
        public decimal? AvailableCloseQuantity { get; set; }
        /// <summary>
        /// Break price
        /// </summary>
        [JsonPropertyName("breakPrice")]
        public decimal BreakPrice { get; set; }
        /// <summary>
        /// Calculated mark price
        /// </summary>
        [JsonPropertyName("calMarkPrice")]
        public decimal CalculatedMarkPrice { get; set; }
        /// <summary>
        /// Quantity of open order
        /// </summary>
        [JsonPropertyName("closeOrderSize")]
        public decimal CloseOrderQuantity { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ProductType ContractType { get; set; }
        /// <summary>
        /// Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("floatingPL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// Open order quantity
        /// </summary>
        [JsonPropertyName("openOrderSize")]
        public decimal OpenOrderQuantity { get; set; }
        /// <summary>
        /// Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// Take profit / stop loss id
        /// </summary>
        [JsonPropertyName("profitId")]
        public long? TpSlId { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedProfit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public PriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal? TriggerProfitPrice { get; set; }
        /// <summary>
        /// Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal? TriggerStopPrice { get; set; }
        /// <summary>
        /// Welfare account
        /// </summary>
        [JsonPropertyName("welfareAccount")]
        public bool WelfareAccount { get; set; }
    }


}
