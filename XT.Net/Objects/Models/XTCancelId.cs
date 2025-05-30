using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Cancel id
    /// </summary>
    [SerializationModel]
    public record XTCancelId
    {
        /// <summary>
        /// Cancel id
        /// </summary>
        [JsonPropertyName("cancelId")]
        public string CancelId { get; set; } = string.Empty;
    }


}
