using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Client IP
    /// </summary>
    public record XTClientIp
    {
        /// <summary>
        /// Ip
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; } = string.Empty;
    }


}
