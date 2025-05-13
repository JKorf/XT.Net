using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Underlying type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<UnderlyingType>))]
    public enum UnderlyingType
    {
        /// <summary>
        /// Usdt based
        /// </summary>
        [Map("U_BASED", "2")]
        UsdtBased,
        /// <summary>
        /// Coin based
        /// </summary>
        [Map("C_BASED", "COIN_BASED", "1")]
        CoinBased
    }
}
