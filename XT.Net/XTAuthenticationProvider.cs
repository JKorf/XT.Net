using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;

namespace XT.Net
{
    internal class XTAuthenticationProvider : AuthenticationProvider
    {
        private static readonly IMessageSerializer _serializer = new SystemTextJsonMessageSerializer();

        public XTAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void AuthenticateRequest(
            RestApiClient apiClient,
            Uri uri,
            HttpMethod method,
            ref IDictionary<string, object>? uriParameters,
            ref IDictionary<string, object>? bodyParameters,
            ref Dictionary<string, string>? headers,
            bool auth,
            ArrayParametersSerialization arraySerialization,
            HttpMethodParameterPosition parameterPosition,
            RequestBodyFormat requestBodyFormat)
        {
            headers = new Dictionary<string, string>() { };

            if (!auth)
                return;

            headers.Add("validate-algorithms", "HmacSHA256");
            headers.Add("validate-appkey", ApiKey);
            var timestamp = GetMillisecondTimestamp(apiClient);
#warning setting
            headers.Add("validate-recvwindow", "5000");
            headers.Add("validate-timestamp", timestamp);

            var body = bodyParameters == null ? string.Empty : GetSerializedBody(_serializer, bodyParameters);
            var signStr = string.Join("&", headers.Select(x => x.Key + "=" + x.Value)) + "#" + method.ToString() + "#" + uri.AbsolutePath;
            if (uriParameters?.Any() == true)
                signStr += "#" + uriParameters!.CreateParamString(false, arraySerialization);
            if (bodyParameters?.Any() == true)
                signStr += "#" + GetSerializedBody(_serializer, bodyParameters);

            var sign = SignHMACSHA256(signStr).ToLower();

            headers.Add("validate-signature", sign);
        }
    }
}
