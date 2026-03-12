using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderStatus>))]
    public enum OrderStatus
    {
        /// <summary>
        /// ["<c>NEW</c>"] New
        /// </summary>
        [Map("NEW")]
        New,
        /// <summary>
        /// ["<c>PARTIALLY_FILLED</c>"] Partially filled
        /// </summary>
        [Map("PARTIALLY_FILLED")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>FILLED</c>"] Filled
        /// </summary>
        [Map("FILLED")]
        Filled,
        /// <summary>
        /// ["<c>CANCELED</c>"] Canceled
        /// </summary>
        [Map("CANCELED")]
        Canceled,
        /// <summary>
        /// ["<c>REJECTED</c>"] Rejected
        /// </summary>
        [Map("REJECTED")]
        Rejected,
        /// <summary>
        /// ["<c>EXPIRED</c>"] Expired
        /// </summary>
        [Map("EXPIRED")]
        Expired,
    }

}
