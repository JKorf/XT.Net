using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal record XTServerTime
    {
        [JsonPropertyName("serverTime")]
        public DateTime ServerTime { get; set; }
    }
}
