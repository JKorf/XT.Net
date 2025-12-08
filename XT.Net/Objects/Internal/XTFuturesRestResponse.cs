using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    [SerializationModel]
    internal record XTFuturesRestResponse
    {
        [JsonPropertyName("returnCode")]
        public int ReturnCode { get; set; }
        [JsonPropertyName("msgInfo")]
        public string MessageCode { get; set; } = string.Empty;
        [JsonPropertyName("error")]
        public XTFuturesError? Error { get; set; }
    }

    [SerializationModel]
    internal record XTFuturesError
    {
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
    }

    [SerializationModel]
    internal record XTFuturesRestResponse<T> : XTFuturesRestResponse
    {
        [JsonPropertyName("result")]
        public T? Result { get; set; }
    }
}
