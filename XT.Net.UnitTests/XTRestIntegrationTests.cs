using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using XT.Net.Clients;
using XT.Net.Objects.Options;
using XT.Net.SymbolOrderBooks;

namespace XT.Net.UnitTests
{
    [NonParallelizable]
    public class XTRestIntegrationTests : RestIntegrationTest<XTRestClient>
    {
        public override bool Run { get; set; }

        public override XTRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new XTRestClient(null, loggerFactory, Options.Create(new XTRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetTickersAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Message, Is.EqualTo("SYMBOL_001"));
        }

        [Test]
        public async Task TestSpotAccountData()
        {
            await RunAndCheckResult(client => client.SpotApi.Account.GetBalanceAsync("usdt", default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetDepositHistoryAsync("USDT", "BNB Smart Chain", default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Account.GetWithdrawalHistoryAsync("USDT", "BNB Smart Chain", default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetClientIpAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetSymbolsAsync(default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetOrderBookAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetKlinesAsync("eth_usdt", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("eth_usdt", default, default, default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetTickersAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetPriceTickersAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetBookTickersAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.Get24HTickersAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetsAsync(default), false);
            await RunAndCheckResult(client => client.SpotApi.ExchangeData.GetAssetNetworksAsync(default), false);
        }

        [Test]
        public async Task TestSpotTradingData()
        {
            await RunAndCheckResult(client => client.SpotApi.Trading.GetOpenOrdersAsync(default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestFuturesAccountData()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetAccountInfoAsync(default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetUserAssetAsync("usdt", default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetUserAssetsAsync(default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetAccountBillsAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetFundingFeeHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetFeeRateAsync(default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Account.GetAdlInfoAsync(default), true);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetClientIpAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSymbolAssetsAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSymbolAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSymbolsAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetLeverageBracketsAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetLeverageBracketsAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTickerAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("eth_usdt", default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetIndexPriceAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetIndexPricesAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetMarkPriceAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetMarkPricesAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("eth_usdt", Enums.FuturesKlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetMarketInfoAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetMarketInfosAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetBookTickersAsync(default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateHistoryAsync("eth_usdt", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetRiskBalanceAsync("eth_usdt", default, default, default, default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetOpenInterestAsync("eth_usdt", default), false);
            await RunAndCheckResult(client => client.UsdtFuturesApi.ExchangeData.GetSymbolInfoAsync(default), false);
        }

        [Test]
        public async Task TestFuturesTradingData()
        {
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetClosedOrdersAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOrdersAsync(default, default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetPositionsAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetPositionsInfoAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetMarginCallInfoAsync(default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetTriggerOrdersAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetClosedTriggerOrdersAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetStopLimitOrdersAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetOpenTrackOrdersAsync(default, default, default, default, default, default), true);
            await RunAndCheckResult(client => client.UsdtFuturesApi.Trading.GetClosedTrackOrdersAsync(default, default, default, default, default, default, default, default), true);
        }

        [Test]
        public async Task TestOrderBooks()
        {
            await TestOrderBook(new XTSpotSymbolOrderBook("eth_usdt"));
            await TestOrderBook(new XTCoinFuturesSymbolOrderBook("eth_usd"));
            await TestOrderBook(new XTUsdtFuturesSymbolOrderBook("eth_usdt"));
        }
    }
}
