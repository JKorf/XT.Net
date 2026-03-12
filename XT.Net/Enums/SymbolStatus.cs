using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SymbolStatus>))]
    public enum SymbolStatus
    {
        /// <summary>
        /// ["<c>ONLINE</c>"] Online
        /// </summary>
        [Map("ONLINE", "0")]
        Online,
        /// <summary>
        /// ["<c>OFFLINE</c>"] Offline
        /// </summary>
        [Map("OFFLINE", "1")]
        Offline,
        /// <summary>
        /// ["<c>DELISTED</c>"] Delisted
        /// </summary>
        [Map("DELISTED")]
        Delisted
    }
}
