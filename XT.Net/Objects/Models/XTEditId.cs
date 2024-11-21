using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace XT.Net.Objects.Models
{
    /// <summary>
    /// Edit id
    /// </summary>
    public record XTEditId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
        /// <summary>
        /// Modify id
        /// </summary>
        [JsonPropertyName("modifyId")]
        public long ModifyId { get; set; }
    }
}
