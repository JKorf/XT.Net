using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Text.Json.Serialization;
using XT.Net.Enums;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Symbol info
    /// </summary>
    [SerializationModel]
    public record XTFuturesSymbolInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Volume in base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// Delivery date
        /// </summary>
        [JsonPropertyName("end_timestamp")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// 24h high price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// Index asset
        /// </summary>
        [JsonPropertyName("index_currency")]
        public string IndexAsset { get; set; } = string.Empty;
        /// <summary>
        /// Index name
        /// </summary>
        [JsonPropertyName("index_name")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// 24h low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// Next funding rate
        /// </summary>
        [JsonPropertyName("next_funding_rate")]
        public decimal? NextFundingRate { get; set; }
        /// <summary>
        /// Next funding rate timestamp
        /// </summary>
        [JsonPropertyName("next_funding_rate_timestamp")]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public ContractType ProductType { get; set; }
        /// <summary>
        /// List time
        /// </summary>
        [JsonPropertyName("start_timestamp")]
        public DateTime? ListTime { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Target asset
        /// </summary>
        [JsonPropertyName("target_currency")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// Target volume
        /// </summary>
        [JsonPropertyName("target_volume")]
        public decimal TargetVolume { get; set; }
        /// <summary>
        /// Ticker id
        /// </summary>
        [JsonPropertyName("ticker_id")]
        public string TickerId { get; set; } = string.Empty;
        /// <summary>
        /// Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
    }


}
