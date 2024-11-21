using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal class XTSocketResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("msg")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
    }
}
