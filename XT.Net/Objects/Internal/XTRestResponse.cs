using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal record XTRestResponse
    {
        [JsonPropertyName("rc")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("mc")]
        public string MessageCode { get; set; } = string.Empty;

        [JsonPropertyName("ma")]
        public object[] MessageArgs { get; set; } = [];
    }

    internal record XTRestResponse<T> : XTRestResponse
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }
}
