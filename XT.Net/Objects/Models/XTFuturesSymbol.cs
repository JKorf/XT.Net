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
        /// ["<c>time</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Data version
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbols</c>"] Symbols
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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>symbolGroupId</c>"] Symbol group id
        /// </summary>
        [JsonPropertyName("symbolGroupId")]
        public int? SymbolGroupId { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contractType</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contractType")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>productType</c>"] Product type
        /// </summary>
        [JsonPropertyName("productType")]
        public ProductType ProductType { get; set; }
        /// <summary>
        /// ["<c>predictEventType</c>"] Predict event type
        /// </summary>
        [JsonPropertyName("predictEventType")]
        public string? PredictEventType { get; set; }
        /// <summary>
        /// ["<c>predictEventParam</c>"] Predict event param
        /// </summary>
        [JsonPropertyName("predictEventParam")]
        public string? PredictEventParam { get; set; }
        /// <summary>
        /// ["<c>predictEventSort</c>"] Predict event sort
        /// </summary>
        [JsonPropertyName("predictEventSort")]
        public string? PredictEventSort { get; set; }
        /// <summary>
        /// ["<c>underlyingType</c>"] Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
        /// <summary>
        /// ["<c>contractSize</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>tradeSwitch</c>"] Trade switch
        /// </summary>
        [JsonPropertyName("tradeSwitch")]
        public bool TradeSwitch { get; set; }
        /// <summary>
        /// ["<c>openSwitch</c>"] Open switch
        /// </summary>
        [JsonPropertyName("openSwitch")]
        public bool OpenSwitch { get; set; }
        /// <summary>
        /// ["<c>isDisplay</c>"] Whether symbol should be displayed
        /// </summary>
        [JsonPropertyName("isDisplay")]
        public bool IsDisplay { get; set; }
        /// <summary>
        /// ["<c>isOpenApi</c>"] Is open api
        /// </summary>
        [JsonPropertyName("isOpenApi")]
        public bool IsOpenApi { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>initLeverage</c>"] Initial leverage
        /// </summary>
        [JsonPropertyName("initLeverage")]
        public decimal InitialLeverage { get; set; }
        /// <summary>
        /// ["<c>initPositionType</c>"] Initial position type
        /// </summary>
        [JsonPropertyName("initPositionType")]
        public PositionType InitialPositionType { get; set; }
        /// <summary>
        /// ["<c>baseCoin</c>"] Base asset
        /// </summary>
        [JsonPropertyName("baseCoin")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>spotCoin</c>"] Spot asset
        /// </summary>
        [JsonPropertyName("spotCoin")]
        public string SpotAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>quoteCoin</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quoteCoin")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>baseCoinPrecision</c>"] Base asset precision
        /// </summary>
        [JsonPropertyName("baseCoinPrecision")]
        public int BaseAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>baseCoinDisplayPrecision</c>"] Base asset display precision
        /// </summary>
        [JsonPropertyName("baseCoinDisplayPrecision")]
        public int BaseAssetDisplayPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteCoinPrecision</c>"] Quote asset precision
        /// </summary>
        [JsonPropertyName("quoteCoinPrecision")]
        public int QuoteAssetPrecision { get; set; }
        /// <summary>
        /// ["<c>quoteCoinDisplayPrecision</c>"] Quote asset display precision
        /// </summary>
        [JsonPropertyName("quoteCoinDisplayPrecision")]
        public int QuoteAssetDisplayPrecision { get; set; }
        /// <summary>
        /// ["<c>quantityPrecision</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("quantityPrecision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>pricePrecision</c>"] Price precision
        /// </summary>
        [JsonPropertyName("pricePrecision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>supportOrderType</c>"] Support order type
        /// </summary>
        [JsonPropertyName("supportOrderType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<OrderType>))]
        public OrderType[] SupportOrderType { get; set; } = [];
        /// <summary>
        /// ["<c>supportTimeInForce</c>"] Support time in force
        /// </summary>
        [JsonPropertyName("supportTimeInForce")]
        [JsonConverter(typeof(CommaSplitEnumConverter<TimeInForce>))]
        public TimeInForce[] SupportTimeInForce { get; set; } = [];
        /// <summary>
        /// ["<c>supportEntrustType</c>"] Support entrust type
        /// </summary>
        [JsonPropertyName("supportEntrustType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<EntrustType>))]
        public EntrustType[] SupportEntrustType { get; set; } = [];
        /// <summary>
        /// ["<c>supportPositionType</c>"] Support position type
        /// </summary>
        [JsonPropertyName("supportPositionType")]
        [JsonConverter(typeof(CommaSplitEnumConverter<PositionType>))]
        public PositionType[] SupportPositionType { get; set; } = [];
        /// <summary>
        /// ["<c>minQty</c>"] Min order quantity
        /// </summary>
        [JsonPropertyName("minQty")]
        public decimal MinQuantity { get; set; }
        /// <summary>
        /// ["<c>minNotional</c>"] Min notional order value
        /// </summary>
        [JsonPropertyName("minNotional")]
        public decimal MinNotional { get; set; }
        /// <summary>
        /// ["<c>maxNotional</c>"] Max notional order value
        /// </summary>
        [JsonPropertyName("maxNotional")]
        public decimal MaxNotional { get; set; }
        /// <summary>
        /// ["<c>multiplierDown</c>"] Floor percentage of sell limit order
        /// </summary>
        [JsonPropertyName("multiplierDown")]
        public decimal MultiplierDown { get; set; }
        /// <summary>
        /// ["<c>multiplierUp</c>"] Cap percentage of buy limit order
        /// </summary>
        [JsonPropertyName("multiplierUp")]
        public decimal MultiplierUp { get; set; }
        /// <summary>
        /// ["<c>maxOpenOrders</c>"] Max open orders
        /// </summary>
        [JsonPropertyName("maxOpenOrders")]
        public int MaxOpenOrders { get; set; }
        /// <summary>
        /// ["<c>maxEntrusts</c>"] Max entrusts
        /// </summary>
        [JsonPropertyName("maxEntrusts")]
        public int MaxEntrusts { get; set; }
        /// <summary>
        /// ["<c>makerFee</c>"] Maker fee
        /// </summary>
        [JsonPropertyName("makerFee")]
        public decimal MakerFee { get; set; }
        /// <summary>
        /// ["<c>takerFee</c>"] Taker fee
        /// </summary>
        [JsonPropertyName("takerFee")]
        public decimal TakerFee { get; set; }
        /// <summary>
        /// ["<c>liquidationFee</c>"] Liquidation fee
        /// </summary>
        [JsonPropertyName("liquidationFee")]
        public decimal LiquidationFee { get; set; }
        /// <summary>
        /// ["<c>marketTakeBound</c>"] Market order maximum price deviation
        /// </summary>
        [JsonPropertyName("marketTakeBound")]
        public decimal MarketTakeBound { get; set; }
        /// <summary>
        /// ["<c>depthPrecisionMerge</c>"] Handicap Precision Consolidation
        /// </summary>
        [JsonPropertyName("depthPrecisionMerge")]
        public decimal DepthPrecisionMerge { get; set; }
        /// <summary>
        /// ["<c>labels</c>"] Labels
        /// </summary>
        [JsonPropertyName("labels")]
        public string[] Labels { get; set; } = Array.Empty<string>();
        /// <summary>
        /// ["<c>onboardDate</c>"] Listing time
        /// </summary>
        [JsonPropertyName("onboardDate")]
        public DateTime ListTime { get; set; }
        /// <summary>
        /// ["<c>enName</c>"] English name
        /// </summary>
        [JsonPropertyName("enName")]
        public string EnglishName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cnName</c>"] Chinese name
        /// </summary>
        [JsonPropertyName("cnName")]
        public string ChineseName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>minStepPrice</c>"] Min step price
        /// </summary>
        [JsonPropertyName("minStepPrice")]
        public decimal MinStepPrice { get; set; }
        /// <summary>
        /// ["<c>minPrice</c>"] Min price
        /// </summary>
        [JsonPropertyName("minPrice")]
        public decimal? MinPrice { get; set; }
        /// <summary>
        /// ["<c>maxPrice</c>"] Max price
        /// </summary>
        [JsonPropertyName("maxPrice")]
        public decimal? MaxPrice { get; set; }
        /// <summary>
        /// ["<c>deliveryDate</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("deliveryDate")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>deliveryPrice</c>"] Delivery price
        /// </summary>
        [JsonPropertyName("deliveryPrice")]
        public decimal? DeliveryPrice { get; set; }
        /// <summary>
        /// ["<c>deliveryCompletion</c>"] Delivery completion
        /// </summary>
        [JsonPropertyName("deliveryCompletion")]
        public bool DeliveryCompletion { get; set; }
        /// <summary>
        /// ["<c>cnDesc</c>"] Chinese description
        /// </summary>
        [JsonPropertyName("cnDesc")]
        public string? ChineseDescription { get; set; }
        /// <summary>
        /// ["<c>enDesc</c>"] English description
        /// </summary>
        [JsonPropertyName("enDesc")]
        public string? EnglishDescription { get; set; }
        /// <summary>
        /// ["<c>cnRemark</c>"] Chinese remark
        /// </summary>
        [JsonPropertyName("cnRemark")]
        public string? ChineseRemark { get; set; }
        /// <summary>
        /// ["<c>enRemark</c>"] English remark
        /// </summary>
        [JsonPropertyName("enRemark")]
        public string? EnglishRemark { get; set; }
        /// <summary>
        /// ["<c>plates</c>"] Plates
        /// </summary>
        [JsonPropertyName("plates")]
        public int[] Plates { get; set; } = Array.Empty<int>();
        /// <summary>
        /// ["<c>fastTrackCallbackRate1</c>"] Fast track callback rate1
        /// </summary>
        [JsonPropertyName("fastTrackCallbackRate1")]
        public decimal? FastTrackCallbackRate1 { get; set; }
        /// <summary>
        /// ["<c>fastTrackCallbackRate2</c>"] Fast track callback rate2
        /// </summary>
        [JsonPropertyName("fastTrackCallbackRate2")]
        public decimal? FastTrackCallbackRate2 { get; set; }
        /// <summary>
        /// ["<c>minTrackCallbackRate</c>"] Min track callback rate
        /// </summary>
        [JsonPropertyName("minTrackCallbackRate")]
        public decimal? MinTrackCallbackRate { get; set; }
        /// <summary>
        /// ["<c>maxTrackCallbackRate</c>"] Max track callback rate
        /// </summary>
        [JsonPropertyName("maxTrackCallbackRate")]
        public decimal? MaxTrackCallbackRate { get; set; }
        /// <summary>
        /// ["<c>latestPriceDeviation</c>"] Latest price deviation
        /// </summary>
        [JsonPropertyName("latestPriceDeviation")]
        public decimal? LatestPriceDeviation { get; set; }
    }


}
