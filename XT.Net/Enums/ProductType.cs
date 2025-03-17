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
        /// Perpetual
        /// </summary>
        [Map("perpetual", "PERPETUAL")]
        Perpetual,
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures", "PREDICT")]
        Futures
    }
}
