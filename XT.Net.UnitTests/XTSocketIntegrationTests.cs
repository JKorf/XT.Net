using XT.Net.Clients;
using XT.Net.Objects.Options;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XT.Net.Objects.Models;

namespace XT.Net.UnitTests
{
    internal class XTSocketIntegrationTests : SocketIntegrationTest<XTSocketClient>
    {
        public override bool Run { get; set; } = true;

        public XTSocketIntegrationTests()
        {
        }

        public override XTSocketClient GetClient(ILoggerFactory loggerFactory, bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new XTSocketClient(Options.Create(new XTSocketOptions
            {
                UseUpdatedDeserialization = useUpdatedDeserialization,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }), loggerFactory);
        }

        private XTRestClient GetRestClient(bool useUpdatedDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new XTRestClient(x =>
            {
                x.UseUpdatedDeserialization = useUpdatedDeserialization;
                x.ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null;
            });
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSubscriptions(bool useUpdatedDeserialization)
        {
            var listenKey = await GetRestClient(useUpdatedDeserialization).SpotApi.Account.GetWebsocketTokenAsync();
            await RunAndCheckUpdate<XT24HTicker>(useUpdatedDeserialization , (client, updateHandler) => client.SpotApi.SubscribeToBalanceUpdatesAsync(listenKey.Data, default, default), false, true);
            await RunAndCheckUpdate<XT24HTicker>(useUpdatedDeserialization, (client, updateHandler) => client.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", updateHandler, default), true, false);
             
            listenKey = await GetRestClient(useUpdatedDeserialization).UsdtFuturesApi.Account.GetListenKeyAsync();
            await RunAndCheckUpdate<XTFuturesTicker>(useUpdatedDeserialization, (client, updateHandler) => client.FuturesApi.SubscribeToBalancesUpdatesAsync(listenKey.Data, default, default), false, true);
            await RunAndCheckUpdate<XTFuturesTicker>(useUpdatedDeserialization, (client, updateHandler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("eth_usdt", updateHandler, default), true, false);
        } 
    }
}
