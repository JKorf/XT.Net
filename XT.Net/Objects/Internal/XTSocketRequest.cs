using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal class XTSocketRequest
    {
        [JsonPropertyName("method")]
        public string Method { get; set; } = string.Empty;
        [JsonPropertyName("params")]
        public string[] Parameters { get; set; } = [];
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("listenKey"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public string? ListenKey { get; set; }
    }
}
