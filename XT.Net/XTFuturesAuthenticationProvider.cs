using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using XT.Net.Clients.SpotApi;

namespace XT.Net
{
    internal class XTFuturesAuthenticationProvider : AuthenticationProvider
    {
        private static readonly IMessageSerializer _serializer = new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        public XTFuturesAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            request.Headers.Add("validate-appkey", ApiKey);
            request.Headers.Add("validate-timestamp", timestamp);

            var body = !request.BodyParameters.Any() ? string.Empty : GetSerializedBody(_serializer, request.BodyParameters);
            var queryString = request.GetQueryString(false);
            var signStr = $"{string.Join("&", request.Headers.Select(x => x.Key + "=" + x.Value))}#{request.Path}";
            if (!string.IsNullOrEmpty(queryString))
                signStr += $"#{queryString}";
            if (!string.IsNullOrEmpty(body))
                signStr += $"#{body}";

            request.Headers.Add("validate-signature", SignHMACSHA256(signStr).ToLower());

            request.SetBodyContent(body);
            request.SetQueryString(queryString);
        }
    }
}
