using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Notification type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<NotificationType>))]
    public enum NotificationType
    {
        /// <summary>
        /// Warning, about to be levelled
        /// </summary>
        [Map("WARN")]
        Warning,
        /// <summary>
        /// Partially liquidation
        /// </summary>
        [Map("PARTIAL")]
        PartialLiquidation,
        /// <summary>
        /// Liquidation
        /// </summary>
        [Map("LIQUIDATION")]
        Liquidation,
        /// <summary>
        /// ADL
        /// </summary>
        [Map("ADL")]
        AutoDeleverage,
    }

}
