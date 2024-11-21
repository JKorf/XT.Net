using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal record XTFuturesRestResponse
    {
        [JsonPropertyName("returnCode")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("msgInfo")]
        public string MessageCode { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public XTFuturesError? Error { get; set; }
    }

    internal record XTFuturesError
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
    }

    internal record XTFuturesRestResponse<T> : XTFuturesRestResponse
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }
}
