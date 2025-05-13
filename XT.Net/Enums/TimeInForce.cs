using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Time in force
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TimeInForce>))]
    public enum TimeInForce
    {
        /// <summary>
        /// Good until canceled
        /// </summary>
        [Map("GTC")]
        GoodTillCanceled,
        /// <summary>
        /// Immediate or cancel, cancel any part that can not be executed immediately
        /// </summary>
        [Map("IOC")]
        ImmediateOrCancel,
        /// <summary>
        /// Fill or kill, immediately fill the entire order or cancel it
        /// </summary>
        [Map("FOK")]
        FillOrKill,
        /// <summary>
        /// Post only
        /// </summary>
        [Map("GTX")]
        PostOnly
    }
}
