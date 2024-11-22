using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XT.Net.Clients;
using XT.Net.Enums;

namespace XT.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new XTRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<XTRestClient>(client, "Endpoints/Spot/Account", "https://sapi.xt.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositAddressAsync("123", "123"), "GetDepositAddress", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetDepositHistoryAsync("123", "123"), "GetDepositHistory", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Account.WithdrawAsync("123", "123", "123", 0.1m), "Withdraw", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawalHistoryAsync("123", "123"), "GetWithdrawalHistory", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync("123", BusinessType.Spot, BusinessType.UsdtFutures, 0.1m, "123"), "Transfer", nestedJsonProperty: "result");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new XTRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<XTRestClient>(client, "Endpoints/Spot/ExchangeData", "https://sapi.xt.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetClientIpAsync(), "GetClientIp", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetSymbolsAsync(), "GetSymbols", nestedJsonProperty: "result", ignoreProperties: ["timeInForces"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("btc_usdt"), "GetOrderBook", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("123", Enums.KlineInterval.OneDay), "GetKlines", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("123"), "GetRecentTrades", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("123"), "GetTradeHistory", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickersAsync("123"), "GetTickers", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPriceTickersAsync(), "GetPriceTickers", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookTickersAsync(), "GetBookTickers", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.Get24HTickersAsync(), "Get24HTicker", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetsAsync(), "GetAssets", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAssetNetworksAsync(), "GetAssetNetworks", nestedJsonProperty: "result");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new XTRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<XTRestClient>(client, "Endpoints/Spot/Trading", "https://sapi.xt.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("123", OrderSide.Buy, OrderType.Market, TimeInForce.ImmediateOrCancel, BusinessType.Spot), "PlaceOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync(123), "GetOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync(123), "CancelOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync(), "GetOpenOrders", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync(BusinessType.Spot), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetClosedOrdersAsync(), "GetClosedOrders", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.EditOrderAsync(123, 0.1m, 0.1m), "EditOrder", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync([123]), "GetOrders", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync(), "GetUserTrades", nestedJsonProperty: "result");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new XTRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<XTRestClient>(client, "Endpoints/Futures/ExchangeData", "https://fapi.xt.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSymbolAsync("123"), "GetSymbol", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetLeverageBracketsAsync("123"), "GetLeverageBrackets", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetTickerAsync("123"), "GetTicker", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("123"), "GetRecentTrades", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetIndexPricesAsync(), "GetIndexPrices", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("123", FuturesKlineInterval.OneHour), "GetKlines", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetMarketInfoAsync("123"), "GetMarketInfo", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("123"), "GetFundingRate", nestedJsonProperty: "result.items");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync("123"), "GetBookTicker", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetFundingRateHistoryAsync("123"), "GetFundingRateHistory", nestedJsonProperty: "result.items");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetRiskBalanceAsync("123"), "GetRiskBalance", nestedJsonProperty: "result.items");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetOpenInterestAsync("123"), "GetOpenInterest", nestedJsonProperty: "result");
            await tester.ValidateAsync(client => client.UsdtFuturesApi.ExchangeData.GetSymbolInfoAsync(), "GetSymbolInfo");
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new XTRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<XTRestClient>(client, "Endpoints/Futures/Account", "https://fapi.xt.com", IsAuthenticated, stjCompare: true);
            await tester.ValidateAsync(client => client.UsdtFuturesApi.Account.GetBalancesAsync(), "GetBalances", nestedJsonProperty: "result");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestHeaders.Any(x => x.Key == "validate-signature");
        }
    }
}
