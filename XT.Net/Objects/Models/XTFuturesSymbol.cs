using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Symbols data
    /// </summary>
    [SerializationModel]
    public record XTFuturesSymbols
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
        public XTFuturesSymbol[] Symbols { get; set; } = Array.Empty<XTFuturesSymbol>();
    }

    /// <summary>
    /// Futures symbol
    /// </summary>
    [SerializationModel]
    public record XTFuturesSymbol
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Symbol group id
        /// </summary>
        [JsonPropertyName("symbolGroupId")]
        public int? SymbolGroupId { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("productType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// Predict event type
        /// </summary>
        [JsonPropertyName("predictEventType")]
        public string? PredictEventType { get; set; }
        /// <summary>
        /// Predict event param
        /// </summary>
        [JsonPropertyName("predictEventParam")]
        public string? PredictEventParam { get; set; }
        /// <summary>
        /// Predict event sort
        /// </summary>
        [JsonPropertyName("predictEventSort")]
        public string? PredictEventSort { get; set; }
        /// <summary>
        /// Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Trade switch
        /// </summary>
        [JsonPropertyName("tradeSwitch")]
        public bool TradeSwitch { get; set; }
        /// <summary>
        /// Open switch
        /// </summary>
        [JsonPropertyName("openSwitch")]
        public bool OpenSwitch { get; set; }
        /// <summary>
        /// Whether symbol should be displayed
        /// </summary>
        [JsonPropertyName("isDisplay")]
        public bool IsDisplay { get; set; }
        /// <summary>
        /// Is open api
        /// </summary>
        [JsonPropertyName("isOpenApi")]
        public bool IsOpenApi { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// Initial leverage
        /// </summary>
        [JsonPropertyName("initLeverage")]
        public decimal InitialLeverage { get; set; }
        /// <summary>
        /// Initial position type
        /// </summary>
        [JsonPropertyName("initPositionType")]
        public PositionType InitialPositionType { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("baseCoin")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Spot asset
        /// </summary>
        [JsonPropertyName("spotCoin")]
        public string SpotAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("quoteCoin")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Base asset precision
        /// </summary>
        [JsonPropertyName("baseCoinPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// Base asset display precision
        /// </summary>
        [JsonPropertyName("baseCoinDisplayPrecision")]
        public int BaseAssetDisplayPrecision { get; set; }
        /// <summary>
        /// Quote asset precision
        /// </summary>
        [JsonPropertyName("quoteCoinPrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// Quote asset display precision
        /// </summary>
        [JsonPropertyName("quoteCoinDisplayPrecision")]
        public int QuoteAssetDisplayPrecision { get; set; }
        /// <summary>
        /// Quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Price precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// Support order type
        /// </summary>
        [JsonPropertyName("supportOrderType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<OrderType>))]
        public OrderType[] SupportOrderType { get; set; } = [];
        /// <summary>
        /// Support time in force
        /// </summary>
        [JsonPropertyName("supportTimeInForce")]
        [JsonConverter(typeof(CommaSplitEnumConverter<TimeInForce>))]
        public TimeInForce[] SupportTimeInForce { get; set; } = [];
        /// <summary>
        /// Support entrust type
        /// </summary>
        [JsonPropertyName("supportEntrustType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<EntrustType>))]
        public EntrustType[] SupportEntrustType { get; set; } = [];
        /// <summary>
        /// Support position type
        /// </summary>
        [JsonPropertyName("supportPositionType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<PositionType>))]
        public PositionType[] SupportPositionType { get; set; } = [];
        /// <summary>
        /// Min order quantity
        /// </summary>
        [JsonPropertyName("minQty")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// Min notional order value
        /// </summary>
        [JsonPropertyName("minNotional")]
        public decimal MinNotional { get; set; }
        /// <summary>
        /// Max notional order value
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// Floor percentage of sell limit order
        /// </summary>
        [JsonPropertyName("multiplierDown")]
        public decimal MultiplierDown { get; set; }
        /// <summary>
        /// Cap percentage of buy limit order
        /// </summary>
        [JsonPropertyName("multiplierUp")]
        public decimal MultiplierUp { get; set; }
        /// <summary>
        /// Max open orders
        /// </summary>
        [JsonPropertyName("maxOpenOrders")]
        public int MaxOpenOrders { get; set; }
        /// <summary>
        /// Max entrusts
        /// </summary>
        [JsonPropertyName("maxEntrusts")]
        public int MaxEntrusts { get; set; }
        /// <summary>
        /// Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// Market order maximum price deviation
        /// </summary>
        [JsonPropertyName("marketTakeBound")]
        public decimal MarketTakeBound { get; set; }
        /// <summary>
        /// Handicap Precision Consolidation
        /// </summary>
        [JsonPropertyName("depthPrecisionMerge")]
        public decimal DepthPrecisionMerge { get; set; }
        /// <summary>
        /// Labels
        /// </summary>
        [JsonPropertyName("labels")]
        public string[] Labels { get; set; } = Array.Empty<string>();
        /// <summary>
        /// Listing time
        /// </summary>
        [JsonPropertyName("onboardDate")]
        public DateTime ListTime { get; set; }
        /// <summary>
        /// English name
        /// </summary>
        [JsonPropertyName("enName")]
        public string EnglishName { get; set; } = string.Empty;
        /// <summary>
        /// Chinese name
        /// </summary>
        [JsonPropertyName("cnName")]
        public string ChineseName { get; set; } = string.Empty;
        /// <summary>
        /// Min step price
        /// </summary>
        [JsonPropertyName("minStepPrice")]
        public decimal MinStepPrice { get; set; }
        /// <summary>
        /// Min price
        /// </summary>
        [JsonPropertyName("minPrice")]
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// Max price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal? MaxPrice { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonPropertyName("deliveryDate")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Delivery price
        /// </summary>
        [JsonPropertyName("deliveryPrice")]
        public decimal? DeliveryPrice { get; set; }
        /// <summary>
        /// Delivery completion
        /// </summary>
        [JsonPropertyName("deliveryCompletion")]
        public bool DeliveryCompletion { get; set; }
        /// <summary>
        /// Chinese description
        /// </summary>
        [JsonPropertyName("cnDesc")]
        public string? ChineseDescription { get; set; }
        /// <summary>
        /// English description
        /// </summary>
        [JsonPropertyName("enDesc")]
        public string? EnglishDescription { get; set; }
        /// <summary>
        /// Chinese remark
        /// </summary>
        [JsonPropertyName("cnRemark")]
        public string? ChineseRemark { get; set; }
        /// <summary>
        /// English remark
        /// </summary>
        [JsonPropertyName("enRemark")]
        public string? EnglishRemark { get; set; }
        /// <summary>
        /// Plates
        /// </summary>
        [JsonPropertyName("plates")]
        public int[] Plates { get; set; } = Array.Empty<int>();
        /// <summary>
        /// Fast track callback rate1
        /// </summary>
        [JsonPropertyName("fastTrackCallbackRate1")]
        public decimal? FastTrackCallbackRate1 { get; set; }
        /// <summary>
        /// Fast track callback rate2
        /// </summary>
        [JsonPropertyName("fastTrackCallbackRate2")]
        public decimal? FastTrackCallbackRate2 { get; set; }
        /// <summary>
        /// Min track callback rate
        /// </summary>
        [JsonPropertyName("minTrackCallbackRate")]
        public decimal? MinTrackCallbackRate { get; set; }
        /// <summary>
        /// Max track callback rate
        /// </summary>
        [JsonPropertyName("maxTrackCallbackRate")]
        public decimal? MaxTrackCallbackRate { get; set; }
        /// <summary>
        /// Latest price deviation
        /// </summary>
        [JsonPropertyName("latestPriceDeviation")]
        public decimal? LatestPriceDeviation { get; set; }
    }


}
