using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Converters;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// A filter for order placed on a symbol.
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTSymbolFilter>))]
    [SerializationModel]
    public record XTSymbolFilter
    {
        /// <summary>
        /// The type of this filter
        /// </summary>
        [JsonPropertyName("filterType")]
        public SymbolFilterType FilterType { get; set; }
    }

    /// <summary>
    /// Price filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTPriceFilter>))]
    [SerializationModel]
    public record XTPriceFilter: XTSymbolFilter
    {
        /// <summary>
        /// The minimal price the order can be for
        /// </summary>
        public decimal MinPrice { get; set; }
        /// <summary>
        /// The max price the order can be for
        /// </summary>
        public decimal MaxPrice { get; set; }
        /// <summary>
        /// The tick size of the price. The price can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Quantity filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTQuantityFilter>))]
    [SerializationModel]
    public record XTQuantityFilter : XTSymbolFilter
    {
        /// <summary>
        /// The minimal quantity of an order
        /// </summary>
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// The max quantity of an order
        /// </summary>
        public decimal? MaxQuantity { get; set; }
        /// <summary>
        /// The tick size of the quantity. The quantity can not have more precision as this and can only be incremented in steps of this.
        /// </summary>
        public decimal TickSize { get; set; }
    }

    /// <summary>
    /// Quote quantity filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTQuoteQuantityFilter>))]
    [SerializationModel]
    public record XTQuoteQuantityFilter : XTSymbolFilter
    {
        /// <summary>
        /// The minimal value of an order in quote quantity
        /// </summary>
        public decimal MinValue { get; set; }
    }

    /// <summary>
    /// Limit protection filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTProtectionLimitFilter>))]
    [SerializationModel]
    public record XTProtectionLimitFilter : XTSymbolFilter
    {
        /// <summary>
        /// The maximum deviation of the buy order, determine the minimum buy order price based on this value and the latest transaction price
        /// </summary>
        public decimal BuyMaxDeviation { get; set; }
        /// <summary>
        /// The buy limit coefficient, determine the maximum buy order price based on this value and the latest transaction price
        /// </summary>
        public decimal BuyPriceLimitCoefficient { get; set; }
        /// <summary>
        /// The maximum deviation of the sell order, determine the maximum sell order price based on this value and the latest transaction price
        /// </summary>
        public decimal SellMaxDeviation { get; set; }
        /// <summary>
        /// The sell limit coefficient, determine the minimum sell order price based on this value and the latest transaction price
        /// </summary>
        public decimal SellPriceLimitCoefficient { get; set; }
    }

    /// <summary>
    /// Market protection filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTProtectionMarketFilter>))]
    [SerializationModel]
    public record XTProtectionMarketFilter : XTSymbolFilter
    {
        /// <summary>
        /// The maximum deviation of the order
        /// </summary>
        public decimal MaxDeviation { get; set; }
    }

    /// <summary>
    /// Online protection filter
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTProtectionOnlineFilter>))]
    [SerializationModel]
    public record XTProtectionOnlineFilter : XTSymbolFilter
    {
        /// <summary>
        /// The maximum price multiple
        /// </summary>
        public decimal MaxPriceMultiple { get; set; }
        /// <summary>
        /// The duration in seconds the protection is active after symbol comes online
        /// </summary>
        public int DurationSeconds { get; set; }
    }
}
