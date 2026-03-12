using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Product type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ProductType>))]
    public enum ProductType
    {
        /// <summary>
        /// ["<c>perpetual</c>"] Perpetual
        /// </summary>
        [Map("perpetual", "PERPETUAL")]
        Perpetual,
        /// <summary>
        /// ["<c>futures</c>"] Futures
        /// </summary>
        [Map("futures", "PREDICT")]
        Futures
    }
}
