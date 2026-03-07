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
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>ask</c>"] Best ask price
        /// </summary>
        [JsonPropertyName("ask")]
        public decimal BestAskPrice { get; set; }
        /// <summary>
        /// ["<c>base_currency</c>"] Base asset
        /// </summary>
        [JsonPropertyName("base_currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>base_volume</c>"] Volume in base asset
        /// </summary>
        [JsonPropertyName("base_volume")]
        public decimal Volume { get; set; }
        /// <summary>
        /// ["<c>bid</c>"] Best bid price
        /// </summary>
        [JsonPropertyName("bid")]
        public decimal BestBidPrice { get; set; }
        /// <summary>
        /// ["<c>contractSize</c>"] Contract size
        /// </summary>
        [JsonPropertyName("contractSize")]
        public decimal ContractSize { get; set; }
        /// <summary>
        /// ["<c>end_timestamp</c>"] Delivery date
        /// </summary>
        [JsonPropertyName("end_timestamp")]
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// ["<c>high</c>"] 24h high price
        /// </summary>
        [JsonPropertyName("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// ["<c>index_currency</c>"] Index asset
        /// </summary>
        [JsonPropertyName("index_currency")]
        public string IndexAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>index_name</c>"] Index name
        /// </summary>
        [JsonPropertyName("index_name")]
        public string IndexName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>last_price</c>"] Last price
        /// </summary>
        [JsonPropertyName("last_price")]
        public decimal? LastPrice { get; set; }
        /// <summary>
        /// ["<c>low</c>"] 24h low price
        /// </summary>
        [JsonPropertyName("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// ["<c>next_funding_rate</c>"] Next funding rate
        /// </summary>
        [JsonPropertyName("next_funding_rate")]
        public decimal? NextFundingRate { get; set; }
        /// <summary>
        /// ["<c>next_funding_rate_timestamp</c>"] Next funding rate timestamp
        /// </summary>
        [JsonPropertyName("next_funding_rate_timestamp")]
        public DateTime? NextFundingTime { get; set; }
        /// <summary>
        /// ["<c>open_interest</c>"] Open interest
        /// </summary>
        [JsonPropertyName("open_interest")]
        public decimal OpenInterest { get; set; }
        /// <summary>
        /// ["<c>product_type</c>"] Product type
        /// </summary>
        [JsonPropertyName("product_type")]
        public ContractType ProductType { get; set; }
        /// <summary>
        /// ["<c>start_timestamp</c>"] List time
        /// </summary>
        [JsonPropertyName("start_timestamp")]
        public DateTime? ListTime { get; set; }
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_currency</c>"] Target asset
        /// </summary>
        [JsonPropertyName("target_currency")]
        public string TargetAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>target_volume</c>"] Target volume
        /// </summary>
        [JsonPropertyName("target_volume")]
        public decimal TargetVolume { get; set; }
        /// <summary>
        /// ["<c>ticker_id</c>"] Ticker id
        /// </summary>
        [JsonPropertyName("ticker_id")]
        public string TickerId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>underlyingType</c>"] Underlying type
        /// </summary>
        [JsonPropertyName("underlyingType")]
        public UnderlyingType UnderlyingType { get; set; }
    }


}
