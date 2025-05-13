using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using XT.Net.Clients;

namespace XT.Net.UnitTests
{
    [TestFixture()]
    public class XTRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new XTSpotAuthenticationProvider(new ApiCredentials("XXX", "XXX"));
            var client = (RestApiClient)new XTRestClient().SpotApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/v4/order",
                (uriParams, bodyParams, headers) =>
                {
                    return headers["validate-signature"].ToString();
                },
                "642fdbb36aae8a672c52c35c621ef1b9f50edbd37a44784a0d264a54ad87ff53",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827319559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<XTRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<XTSocketClient>();
        }
    }
}
