using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Bill side
    /// </summary>
    [JsonConverter(typeof(EnumConverter<BillSide>))]
    public enum BillSide
    {
        /// <summary>
        /// Transfer in
        /// </summary>
        [Map("ADD")]
        TransferIn,
        /// <summary>
        /// Transfer out
        /// </summary>
        [Map("SUB")]
        TransferOut
    }
}
