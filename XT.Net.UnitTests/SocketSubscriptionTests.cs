using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using XT.Net.Clients;
using XT.Net.Objects.Models;
using XT.Net.Objects.Options;

namespace XT.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateSpotSubscriptions(bool useUpdatedDeserialization)
        {
            var client = new XTSocketClient(opts =>
            {
                opts.UseUpdatedDeserialization = useUpdatedDeserialization;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<XTSocketClient>(client, "Subscriptions/Spot", "wss://stream.xt.com");
            await tester.ValidateAsync<XTTradeUpdate>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("eth_usdt", handler), "Trades", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTKlineUpdate>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("eth_usdt", Enums.KlineInterval.FiveMinutes, handler), "Klines", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToOrderBookUpdatesAsync("eth_usdt", 20, handler), "OrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync<XT24HTicker>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTBalanceUpdate>((client, handler) => client.SpotApi.SubscribeToBalanceUpdatesAsync("key", handler), "Balance", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync("key", handler), "Order", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTUserTradeUpdate>((client, handler) => client.SpotApi.SubscribeToUserTradeUpdatesAsync("key", handler), "UserTrade", nestedJsonProperty: "data");
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task ValidateFuturesSubscriptions(bool useUpdatedDeserialization)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            var client = new XTSocketClient(Options.Create(new XTSocketOptions
            {
                OutputOriginalData = true,
                UseUpdatedDeserialization = useUpdatedDeserialization,
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456")
            }), loggerFactory);
            var tester = new SocketSubscriptionValidator<XTSocketClient>(client, "Subscriptions/Futures", "wss://stream.xt.com");
            await tester.ValidateAsync<XTFuturesTrade>((client, handler) => client.FuturesApi.SubscribeToTradeUpdatesAsync("eth_usdt", handler), "Trades", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesKline>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("eth_usdt", Enums.KlineInterval.FiveMinutes, handler), "Klines", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesTicker>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("eth_usdt", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTMarketInfo>((client, handler) => client.FuturesApi.SubscribeToAggregatedTickerUpdatesAsync("eth_usdt", handler), "AggTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTPrice>((client, handler) => client.FuturesApi.SubscribeToIndexPriceUpdatesAsync("eth_usdt", handler), "IndexPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesIncrementalOrderBookUpdate>((client, handler) => client.FuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync("eth_usdt", 100, handler), "OrderBook", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesBalanceUpdate>((client, handler) => client.FuturesApi.SubscribeToBalancesUpdatesAsync("key", handler), "Balances", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesPositionUpdate>((client, handler) => client.FuturesApi.SubscribeToPositionUpdatesAsync("key", handler), "Position", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTFuturesOrder>((client, handler) => client.FuturesApi.SubscribeToOrderUpdatesAsync("key", handler), "Order", nestedJsonProperty: "data", ignoreProperties: new List<string> { "type" });
            await tester.ValidateAsync<XTFuturesUserTradeUpdate>((client, handler) => client.FuturesApi.SubscribeToUserTradeUpdatesAsync("key", handler), "UserTrade", nestedJsonProperty: "data");
            await tester.ValidateAsync<XTNotification>((client, handler) => client.FuturesApi.SubscribeToNotificationUpdatesAsync("key", handler), "Notification", nestedJsonProperty: "data");
        }
    }
}
