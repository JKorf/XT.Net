using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Account VIP level and spot trading fee rates
    /// </summary>
    [SerializationModel]
    public record XTVipFeeInfo
    {
        /// <summary>
        /// ["<c>vipLevel</c>"] VIP level
        /// </summary>
        [JsonPropertyName("vipLevel")]
        public int VipLevel { get; set; }

        /// <summary>
        /// ["<c>vipName</c>"] VIP name, or null when not supplied by the exchange
        /// </summary>
        [JsonPropertyName("vipName")]
        public string? VipName { get; set; }

        /// <summary>
        /// ["<c>makerFeeRate</c>"] Maker fee as a fraction (0.002 = 0.2%)
        /// </summary>
        [JsonPropertyName("makerFeeRate")]
        public decimal MakerFeeRate { get; set; }

        /// <summary>
        /// ["<c>takerFeeRate</c>"] Taker fee as a fraction (0.002 = 0.2%)
        /// </summary>
        [JsonPropertyName("takerFeeRate")]
        public decimal TakerFeeRate { get; set; }
    }
}
