using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;
using XT.Net.Converters;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Daily order limit returned in Spot symbol metadata.
    /// </summary>
    [JsonConverter(typeof(SymbolFilterConverterImp<XTDailyOrderLimitFilter>))]
    [SerializationModel]
    public record XTDailyOrderLimitFilter : XTSymbolFilter
    {
        /// <summary>
        /// ["<c>orderLimited</c>"] Native daily order limit value parsed from its numeric string.
        /// </summary>
        [JsonPropertyName("orderLimited")]
        public decimal OrderLimit { get; set; }
    }
}
