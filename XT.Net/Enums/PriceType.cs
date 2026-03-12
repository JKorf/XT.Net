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
        /// ["<c>INDEX_PRICE</c>"] Index price
        /// </summary>
        [Map("INDEX_PRICE", "1")]
        IndexPrice,
        /// <summary>
        /// ["<c>MARK_PRICE</c>"] Mark price
        /// </summary>
        [Map("MARK_PRICE", "2")]
        MarkPrice,
        /// <summary>
        /// ["<c>LATEST_PRICE</c>"] Last price
        /// </summary>
        [Map("LATEST_PRICE", "3")]
        LastPrice
    }
}
