using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Linq;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Symbols data
    /// </summary>
    [SerializationModel]
    public record XTSymbols
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        /// <summary>
        /// Data version
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// Symbols
        /// </summary>
        [JsonPropertyName("symbols")]
        public XTSymbol[] Symbols { get; set; } = Array.Empty<XTSymbol>();
    }

    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record XTSymbol
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol status
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus SymbolStatus { get; set; }
        /// <summary>
        /// Is trading enabled
        /// </summary>
        [JsonPropertyName("tradingEnabled")]
        public bool TradingEnabled { get; set; }
        /// <summary>
        /// Is OpenApi enabled
        /// </summary>
        [JsonPropertyName("openapiEnabled")]
        public bool OpenapiEnabled { get; set; }
        /// <summary>
        /// Next state time
        /// </summary>
        [JsonPropertyName("nextStateTime")]
        public DateTime? NextStateTime { get; set; }
        /// <summary>
        /// Next state
        /// </summary>
        [JsonPropertyName("nextState")]
        public SymbolStatus? NextState { get; set; }
        /// <summary>
        /// Depth merge precision
        /// </summary>
        [JsonPropertyName("depthMergePrecision")]
        public int DepthMergePrecision { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCurrency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset precision
        /// </summary>
        [JsonPropertyName("baseCurrencyPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// Base asset id
        /// </summary>
        [JsonPropertyName("baseCurrencyId")]
        public long BaseAssetId { get; set; }
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCurrency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset precision
        /// </summary>
        [JsonPropertyName("quoteCurrencyPrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Quote asset id
        /// </summary>
        [JsonPropertyName("quoteCurrencyId")]
        public long QuoteAssetId { get; set; }
        /// <summary>
        /// Price precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// Quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Taker fee rate
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
        /// <summary>
        /// Maker fee rate
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }
        /// <summary>
        /// Supported order types
        /// </summary>
        [JsonPropertyName("orderTypes")]
        public OrderType[] OrderTypes { get; set; } = Array.Empty<OrderType>();
        /// <summary>
        /// Supported time in forces
        /// </summary>
        [JsonPropertyName("timeInForces")]
        public TimeInForce[] TimeInForces { get; set; } = Array.Empty<TimeInForce>();
        /// <summary>
        /// Display weight
        /// </summary>
        [JsonPropertyName("displayWeight")]
        public int DisplayWeight { get; set; }
        /// <summary>
        /// Display level
        /// </summary>
        [JsonPropertyName("displayLevel")]
        public string DisplayLevel { get; set; } = string.Empty;
        /// <summary>
        /// Plates
        /// </summary>
        [JsonPropertyName("plates")]
        public int[] Plates { get; set; } = Array.Empty<int>();
        /// <summary>
        /// Filters for order on this symbol
        /// </summary>
        [JsonPropertyName("filters")]
        public XTSymbolFilter[] Filters { get; set; } = Array.Empty<XTSymbolFilter>();
        /// <summary>
        /// Price filter for this symbol
        /// </summary>
        [JsonIgnore]
        public XTPriceFilter? PriceFilter => Filters.OfType<XTPriceFilter>().FirstOrDefault();
        /// <summary>
        /// Quantity filter for this symbol
        /// </summary>
        [JsonIgnore]
        public XTQuantityFilter? QuantityFilter => Filters.OfType<XTQuantityFilter>().FirstOrDefault();
        /// <summary>
        /// Quote quantity filter for this symbol
        /// </summary>
        [JsonIgnore]
        public XTQuoteQuantityFilter? QuoteQuantityFilter => Filters.OfType<XTQuoteQuantityFilter>().FirstOrDefault();
    }

    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record XTSymbolSUBSUB
    {
        /// <summary>
        /// Filter
        /// </summary>
        [JsonPropertyName("filter")]
        public string Filter { get; set; } = string.Empty;
        /// <summary>
        /// Buy max deviation
        /// </summary>
        [JsonPropertyName("buyMaxDeviation")]
        public decimal BuyMaxDeviation { get; set; }
        /// <summary>
        /// Sell max deviation
        /// </summary>
        [JsonPropertyName("sellMaxDeviation")]
        public decimal SellMaxDeviation { get; set; }
    }


}
