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
        /// ["<c>autoMargin</c>"] Whether to automatically call margin
        /// </summary>
        [JsonPropertyName("autoMargin")]
        public bool AutoMargin { get; set; }
        /// <summary>
        /// ["<c>availableCloseSize</c>"] Available close quantity
        /// </summary>
        [JsonPropertyName("availableCloseSize")]
        public decimal? AvailableCloseQuantity { get; set; }
        /// <summary>
        /// ["<c>breakPrice</c>"] Break price
        /// </summary>
        [JsonPropertyName("breakPrice")]
        public decimal BreakPrice { get; set; }
        /// <summary>
        /// ["<c>calMarkPrice</c>"] Calculated mark price
        /// </summary>
        [JsonPropertyName("calMarkPrice")]
        public decimal CalculatedMarkPrice { get; set; }
        /// <summary>
        /// ["<c>closeOrderSize</c>"] Quantity of open order
        /// </summary>
        [JsonPropertyName("closeOrderSize")]
        public decimal CloseOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ProductType ContractType { get; set; }
        /// <summary>
        /// ["<c>entryPrice</c>"] Average entry price
        /// </summary>
        [JsonPropertyName("entryPrice")]
        public decimal EntryPrice { get; set; }
        /// <summary>
        /// ["<c>floatingPL</c>"] Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("floatingPL")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// ["<c>isolatedMargin</c>"] Isolated margin
        /// </summary>
        [JsonPropertyName("isolatedMargin")]
        public decimal IsolatedMargin { get; set; }
        /// <summary>
        /// ["<c>leverage</c>"] Leverage
        /// </summary>
        [JsonPropertyName("leverage")]
        public decimal Leverage { get; set; }
        /// <summary>
        /// ["<c>openOrderMarginFrozen</c>"] Open order margin frozen
        /// </summary>
        [JsonPropertyName("openOrderMarginFrozen")]
        public decimal OpenOrderMarginFrozen { get; set; }
        /// <summary>
        /// ["<c>openOrderSize</c>"] Open order quantity
        /// </summary>
        [JsonPropertyName("openOrderSize")]
        public decimal OpenOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>positionSide</c>"] Position side
        /// </summary>
        [JsonPropertyName("positionSide")]
        public PositionSide PositionSide { get; set; }
        /// <summary>
        /// ["<c>positionSize</c>"] Position quantity
        /// </summary>
        [JsonPropertyName("positionSize")]
        public decimal PositionSize { get; set; }
        /// <summary>
        /// ["<c>positionType</c>"] Position type
        /// </summary>
        [JsonPropertyName("positionType")]
        public PositionType PositionType { get; set; }
        /// <summary>
        /// ["<c>profitId</c>"] Take profit / stop loss id
        /// </summary>
        [JsonPropertyName("profitId")]
        public long? TpSlId { get; set; }
        /// <summary>
        /// ["<c>realizedProfit</c>"] Realized profit and loss
        /// </summary>
        [JsonPropertyName("realizedProfit")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>triggerPriceType</c>"] Trigger price type
        /// </summary>
        [JsonPropertyName("triggerPriceType")]
        public PriceType? TriggerPriceType { get; set; }
        /// <summary>
        /// ["<c>triggerProfitPrice</c>"] Trigger profit price
        /// </summary>
        [JsonPropertyName("triggerProfitPrice")]
        public decimal? TriggerProfitPrice { get; set; }
        /// <summary>
        /// ["<c>triggerStopPrice</c>"] Trigger stop price
        /// </summary>
        [JsonPropertyName("triggerStopPrice")]
        public decimal? TriggerStopPrice { get; set; }
        /// <summary>
        /// ["<c>welfareAccount</c>"] Welfare account
        /// </summary>
        [JsonPropertyName("welfareAccount")]
        public bool WelfareAccount { get; set; }
    }


}
