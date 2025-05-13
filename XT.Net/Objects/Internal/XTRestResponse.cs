using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    [SerializationModel]
    internal record XTRestResponse
    {
        [JsonPropertyName("rc")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("mc")]
        public string MessageCode { get; set; } = string.Empty;

        [JsonPropertyName("ma")]
        public object[] MessageArgs { get; set; } = [];
    }

    [SerializationModel]
    internal record XTRestResponse<T> : XTRestResponse
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }
}
