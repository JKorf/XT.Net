using System.Text.Json.Serialization;

namespace XT.Net.Objects.Internal
{
    internal class XTToken
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
