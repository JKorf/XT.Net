using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using XT.Net.Clients;
using XT.Net.Objects.Models;

namespace XT.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotExchangeDataSubscriptions()
        {
            var client = new XTSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<XTSocketClient>(client, "Subscriptions/Spot", "XXX", stjCompare: true);
            //await tester.ValidateAsync<XTModel>((client, handler) => client.SpotApi.SubscribeToXXXUpdatesAsync(handler), "XXX");
        }
    }
}
