using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceType>))]
    public enum PriceType
    {
        /// <summary>
        /// Index price
        /// </summary>
        [Map("INDEX_PRICE", "1")]
        IndexPrice,
        /// <summary>
        /// Mark price
        /// </summary>
        [Map("MARK_PRICE", "2")]
        MarkPrice,
        /// <summary>
        /// Last price
        /// </summary>
        [Map("LATEST_PRICE", "3")]
        LastPrice
    }
}
