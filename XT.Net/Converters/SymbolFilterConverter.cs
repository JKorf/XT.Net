using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using XT.Net.Enums;
using XT.Net.Objects.Models;

namespace XT.Net.Converters
{
    internal class SymbolFilterConverterImp<T> : JsonConverter<T>
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var obj = JsonDocument.ParseValue(ref reader).RootElement;
            var type = obj.GetProperty("filter").Deserialize<SymbolFilterType>((JsonTypeInfo<SymbolFilterType>)options.GetTypeInfo(typeof(SymbolFilterType)));
            XTSymbolFilter result;
            switch (type)
            {
                case SymbolFilterType.Price:
                    result = new XTPriceFilter
                    {
                        MaxPrice = ParseDecimal(obj, "max"),
                        MinPrice = ParseDecimal(obj, "min"),
                        TickSize = ParseDecimal(obj, "tickSize")
                    };
                    break;
                case SymbolFilterType.Quantity:
                    result = new XTQuantityFilter
                    {
                        MaxQuantity = ParseDecimal(obj, "max"),
                        MinQuantity = ParseDecimal(obj, "min"),
                        TickSize = ParseDecimal(obj, "tickSize"),
                    };
                    break;
                case SymbolFilterType.QuoteQuantity:
                    result = new XTQuoteQuantityFilter
                    {
                        MinValue = ParseDecimal(obj, "min")
                    };
                    break;
                case SymbolFilterType.ProtectionLimit:
                    result = new XTProtectionLimitFilter
                    {
                        BuyMaxDeviation = ParseDecimal(obj, "buyMaxDeviation"),
                        BuyPriceLimitCoefficient = ParseDecimal(obj, "buyPriceLimitCoefficient"),
                        SellMaxDeviation = ParseDecimal(obj, "sellMaxDeviation"),
                        SellPriceLimitCoefficient = ParseDecimal(obj, "sellPriceLimitCoefficient")
                    };
                    break;
                case SymbolFilterType.ProtectionMarket:
                    result = new XTProtectionMarketFilter
                    {
                        MaxDeviation = ParseDecimal(obj, "maxDeviation")
                    };
                    break;
                case SymbolFilterType.ProtectionOnline:
                    result = new XTProtectionOnlineFilter
                    {
                        MaxPriceMultiple = ParseDecimal(obj, "maxPriceMultiple"),
                        DurationSeconds = int.Parse(obj.GetProperty("durationSeconds").GetString()!, NumberStyles.Float, CultureInfo.InvariantCulture)
                    };
                    break;
                default:
                    Trace.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss:fff} | Warning | Can't parse symbol filter of type: " + obj.GetProperty("filter").GetString());
                    result = new XTSymbolFilter();
                    break;
            }
#pragma warning restore 8604
            result.FilterType = type;
            return (T)(object)result;
        }

        private decimal ParseDecimal(JsonElement element, string property)
        {
            var propFound = element.TryGetProperty(property, out var val);
            if (!propFound)
                return default;

            if (decimal.TryParse(val.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var result))
                return result;

            return default;
        }

#if NET5_0_OR_GREATER
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL3050:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
        [UnconditionalSuppressMessage("AssemblyLoadTrimming", "IL2026:RequiresUnreferencedCode", Justification = "JsonSerializerOptions provided here has TypeInfoResolver set")]
#endif
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, SerializerOptions.WithConverters(XTExchange._serializerContext));
        }
    }
}
