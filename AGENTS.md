---
name: xt-net
description: Use XT.Net when generating C#/.NET code that interacts with the XT cryptocurrency exchange API, including Spot, USDT-M Futures, Coin-M Futures, REST endpoints, WebSocket subscriptions, account balances, market data, and order placement. Triggers on XT integration requests in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C# instead of raw HttpClient.
---

# XT.Net Skill

## Quick Decision

If the user asks for XT API access in C#/.NET, use XT.Net. Do not write raw `HttpClient` calls to XT endpoints; XT.Net handles request signing, timestamping, response parsing, client-side rate limiting, WebSocket reconnects, and `WebCallResult<T>` / `CallResult<T>` error handling.

For multi-exchange code, use `CryptoExchange.Net.SharedApis` from the `.SharedClient` properties.

## Installation

```bash
dotnet add package XT.Net
```

Targets: `netstandard2.0`, `netstandard2.1`, `net8.0`, `net9.0`, `net10.0`. Native AOT is supported.

## REST Client Setup

Always create REST clients via `XTRestClient`. Configure credentials for account and trading calls.

```csharp
using XT.Net;
using XT.Net.Clients;

var restClient = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});
```

Public market data does not require credentials:

```csharp
var publicClient = new XTRestClient();
```

## Result Handling

Every REST method returns `WebCallResult<T>` or `WebCallResult`. Every WebSocket subscription returns `CallResult<UpdateSubscription>`. Always check `.Success` before reading `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
if (!ticker.Success)
{
    Console.WriteLine($"XT error: {ticker.Error}");
    return;
}

var firstTicker = ticker.Data.First();
```

Use `result.Error?.IsTransient == true` as a retry hint. Do not retry validation errors, bad signatures, missing permissions, invalid quantities, or insufficient balance indefinitely.

## API Surface

```csharp
restClient.SpotApi.ExchangeData        // spot market data, symbols, tickers, klines, order books, trades
restClient.SpotApi.Account             // balances, deposits, withdrawals, transfers, websocket token
restClient.SpotApi.Trading             // place/cancel/edit/query spot orders, user trades

restClient.UsdtFuturesApi.ExchangeData // USDT-M futures market data
restClient.UsdtFuturesApi.Account      // USDT-M balances, leverage, margin, fee rate, listen key
restClient.UsdtFuturesApi.Trading      // USDT-M orders, positions, trigger/TP-SL/track orders

restClient.CoinFuturesApi.*            // Coin-M futures with the same futures REST structure

socketClient.SpotApi                   // spot public and private streams
socketClient.FuturesApi                // futures public and private streams
```

Important: XT socket futures are exposed as `socketClient.FuturesApi`; REST futures are split into `UsdtFuturesApi` and `CoinFuturesApi`.

## Spot REST Routing

Spot source examples use XT spot format such as `eth_usdt`.

```csharp
await restClient.SpotApi.ExchangeData.GetServerTimeAsync();
await restClient.SpotApi.ExchangeData.GetSymbolsAsync(symbol: "eth_usdt");
await restClient.SpotApi.ExchangeData.GetOrderBookAsync("eth_usdt", limit: 100);
await restClient.SpotApi.ExchangeData.GetKlinesAsync("eth_usdt", KlineInterval.OneMinute);
await restClient.SpotApi.ExchangeData.GetRecentTradesAsync("eth_usdt");
await restClient.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
await restClient.SpotApi.ExchangeData.GetPriceTickersAsync("eth_usdt");
await restClient.SpotApi.ExchangeData.GetBookTickersAsync("eth_usdt");
await restClient.SpotApi.ExchangeData.Get24HTickersAsync("eth_usdt");
await restClient.SpotApi.ExchangeData.GetAssetsAsync();
await restClient.SpotApi.ExchangeData.GetAssetNetworksAsync();
```

Authenticated spot:

```csharp
await restClient.SpotApi.Account.GetBalancesAsync();
await restClient.SpotApi.Account.GetBalanceAsync("usdt");
await restClient.SpotApi.Account.GetDepositAddressAsync("usdt", "TRC20");
await restClient.SpotApi.Account.GetDepositHistoryAsync("usdt", "TRC20");
await restClient.SpotApi.Account.GetWithdrawalHistoryAsync("usdt", "TRC20");
await restClient.SpotApi.Account.TransferAsync("usdt", BusinessType.Spot, BusinessType.UsdtFutures, 10m, "client-transfer-id");
await restClient.SpotApi.Account.GetWebsocketTokenAsync();
```

## Spot Order Pattern

XT spot order placement requires `TimeInForce` and `BusinessType`.

```csharp
using XT.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "eth_usdt",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    timeInForce: TimeInForce.GoodTillCanceled,
    businessType: BusinessType.Spot,
    quantity: 0.01m,
    price: 2000m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
var orderId = order.Data.OrderId;
```

For spot market buy orders, use `quoteQuantity` instead of `quantity`, as documented by the `PlaceOrderAsync` signature.

Spot trading methods:

```csharp
await restClient.SpotApi.Trading.GetOrderAsync(orderId);
await restClient.SpotApi.Trading.GetOrderByClientOrderIdAsync(clientOrderId);
await restClient.SpotApi.Trading.CancelOrderAsync(orderId);
await restClient.SpotApi.Trading.GetOpenOrdersAsync(symbol: "eth_usdt");
await restClient.SpotApi.Trading.GetClosedOrdersAsync(symbol: "eth_usdt");
await restClient.SpotApi.Trading.CancelAllOrdersAsync(BusinessType.Spot, symbol: "eth_usdt");
await restClient.SpotApi.Trading.EditOrderAsync(orderId, quantity: 0.01m, price: 1990m);
await restClient.SpotApi.Trading.GetUserTradesAsync(symbol: "eth_usdt");
```

## Futures REST Routing

Use `UsdtFuturesApi` for USDT-M futures and `CoinFuturesApi` for Coin-M futures. The futures REST interface shape is shared.

```csharp
await restClient.UsdtFuturesApi.ExchangeData.GetSymbolsAsync();
await restClient.UsdtFuturesApi.ExchangeData.GetSymbolAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH_USDT", FuturesKlineInterval.OneMinute);
await restClient.UsdtFuturesApi.ExchangeData.GetMarkPriceAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetIndexPriceAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH_USDT");
await restClient.UsdtFuturesApi.ExchangeData.GetOpenInterestAsync("ETH_USDT");
```

Authenticated futures:

```csharp
await restClient.UsdtFuturesApi.Account.GetBalancesAsync();
await restClient.UsdtFuturesApi.Account.GetAccountInfoAsync();
await restClient.UsdtFuturesApi.Account.GetUserAssetsAsync();
await restClient.UsdtFuturesApi.Account.GetFeeRateAsync();
await restClient.UsdtFuturesApi.Account.SetLeverageAsync("ETH_USDT", PositionSide.Long, 5);
await restClient.UsdtFuturesApi.Account.AdjustMarginAsync("ETH_USDT", 10m, PositionSide.Long, AdjustSide.Add);
await restClient.UsdtFuturesApi.Account.SetPositionTypeAsync("ETH_USDT", PositionSide.Long, PositionType.Isolated);
await restClient.UsdtFuturesApi.Account.GetListenKeyAsync();
```

## Futures Order Pattern

XT futures symbols in the current trading interfaces use examples like `ETH_USDT`. Futures order placement requires `PositionSide`.

```csharp
using XT.Net.Enums;

await restClient.UsdtFuturesApi.Account.SetLeverageAsync("ETH_USDT", PositionSide.Long, 5);

var order = await restClient.UsdtFuturesApi.Trading.PlaceOrderAsync(
    symbol: "ETH_USDT",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Market,
    quantity: 0.01m,
    positionSide: PositionSide.Long);

if (!order.Success) { Console.WriteLine(order.Error); return; }
```

Futures trading methods:

```csharp
await restClient.UsdtFuturesApi.Trading.GetOrderAsync(orderId);
await restClient.UsdtFuturesApi.Trading.GetOrdersAsync(symbol: "ETH_USDT");
await restClient.UsdtFuturesApi.Trading.CancelOrderAsync(orderId);
await restClient.UsdtFuturesApi.Trading.CancelAllOrdersAsync("ETH_USDT");
await restClient.UsdtFuturesApi.Trading.GetPositionsAsync("ETH_USDT");
await restClient.UsdtFuturesApi.Trading.GetPositionsInfoAsync("ETH_USDT");
await restClient.UsdtFuturesApi.Trading.CloseAllPositionsAsync();
await restClient.UsdtFuturesApi.Trading.PlaceTriggerOrderAsync(...);
await restClient.UsdtFuturesApi.Trading.PlaceStopLimitOrderAsync(...);
await restClient.UsdtFuturesApi.Trading.PlaceTrackOrderAsync(...);
```

Use `CoinFuturesApi` for Coin-M REST calls with the same method groups.

## WebSocket Pattern

Use `XTSocketClient`. Store subscriptions and unsubscribe on shutdown.

```csharp
using XT.Net.Clients;

var socketClient = new XTSocketClient();
var sub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "eth_usdt",
    update => Console.WriteLine(update.Data.LastPrice));

if (!sub.Success) { Console.WriteLine(sub.Error); return; }

await socketClient.UnsubscribeAsync(sub.Data);
```

Public spot streams:

```csharp
await socketClient.SpotApi.SubscribeToTradeUpdatesAsync("eth_usdt", handler);
await socketClient.SpotApi.SubscribeToKlineUpdatesAsync("eth_usdt", KlineInterval.OneMinute, handler);
await socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync("eth_usdt", 20, handler);
await socketClient.SpotApi.SubscribeToIncrementalOrderBookUpdatesAsync("eth_usdt", handler);
await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", handler);
```

Public futures streams:

```csharp
await socketClient.FuturesApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler);
await socketClient.FuturesApi.SubscribeToKlineUpdatesAsync("ETH_USDT", KlineInterval.OneMinute, handler);
await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler);
await socketClient.FuturesApi.SubscribeToAggregatedTickerUpdatesAsync("ETH_USDT", handler);
await socketClient.FuturesApi.SubscribeToIndexPriceUpdatesAsync("ETH_USDT", handler);
await socketClient.FuturesApi.SubscribeToMarkPriceUpdatesAsync("ETH_USDT", handler);
await socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync("ETH_USDT", 20, 100, handler);
await socketClient.FuturesApi.SubscribeToFundingRateUpdatesAsync("ETH_USDT", handler);
```

Private spot streams require a websocket token from REST:

```csharp
var token = await restClient.SpotApi.Account.GetWebsocketTokenAsync();
if (!token.Success) { Console.WriteLine(token.Error); return; }

await socketClient.SpotApi.SubscribeToBalanceUpdatesAsync(token.Data, handler);
await socketClient.SpotApi.SubscribeToOrderUpdatesAsync(token.Data, handler);
await socketClient.SpotApi.SubscribeToUserTradeUpdatesAsync(token.Data, handler);
```

Private futures streams require a listen key:

```csharp
var listenKey = await restClient.UsdtFuturesApi.Account.GetListenKeyAsync();
if (!listenKey.Success) { Console.WriteLine(listenKey.Error); return; }

await socketClient.FuturesApi.SubscribeToBalancesUpdatesAsync(listenKey.Data, handler);
await socketClient.FuturesApi.SubscribeToPositionUpdatesAsync(listenKey.Data, handler);
await socketClient.FuturesApi.SubscribeToOrderUpdatesAsync(listenKey.Data, handler);
await socketClient.FuturesApi.SubscribeToUserTradeUpdatesAsync(listenKey.Data, handler);
await socketClient.FuturesApi.SubscribeToNotificationUpdatesAsync(listenKey.Data, handler);
```

## SharedApis

Use SharedApis for exchange-agnostic code across XT, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

```csharp
using CryptoExchange.Net.SharedApis;
using XT.Net.Clients;

ISpotTickerRestClient shared = new XTRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");
var ticker = await shared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

Available shared clients:

```csharp
new XTRestClient().SpotApi.SharedClient
new XTRestClient().UsdtFuturesApi.SharedClient
new XTRestClient().CoinFuturesApi.SharedClient
new XTSocketClient().SpotApi.SharedClient
new XTSocketClient().FuturesApi.SharedClient
```

Spot REST shared interfaces include `IAssetsRestClient`, `IBalanceRestClient`, `IDepositRestClient`, `IKlineRestClient`, `IListenKeyRestClient`, `IOrderBookRestClient`, `IRecentTradeRestClient`, `IWithdrawalRestClient`, `IWithdrawRestClient`, `ISpotTickerRestClient`, `ISpotSymbolRestClient`, `ISpotOrderRestClient`, `IFeeRestClient`, `IBookTickerRestClient`, and `ITransferRestClient`.

Futures REST shared interfaces include `IBalanceRestClient`, `IKlineRestClient`, `IListenKeyRestClient`, `IOrderBookRestClient`, `IRecentTradeRestClient`, `IFundingRateRestClient`, `IFuturesSymbolRestClient`, `IFuturesTickerRestClient`, `ILeverageRestClient`, `IOpenInterestRestClient`, `IFuturesOrderRestClient`, `IFeeRestClient`, `IFuturesTriggerOrderRestClient`, `IFuturesTpSlRestClient`, and `IBookTickerRestClient`.

## Dependency Injection

```csharp
using XT.Net;

services.AddXT(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});

// Inject IXTRestClient and IXTSocketClient.
```

Configuration binding:

```csharp
services.AddXT(configuration.GetSection("XT"));
```

XT currently exposes `XTEnvironment.Live` and custom environments via `XTEnvironment.CreateCustom(...)`.

## Common Pitfalls

- Do not use raw `HttpClient` to call XT endpoints.
- Do not use `.Data` unless `.Success` is true.
- Do not confuse spot symbols such as `eth_usdt` with futures examples such as `ETH_USDT`.
- Do not use `socketClient.UsdtFuturesApi`; socket futures are under `socketClient.FuturesApi`.
- Do not assume private streams authenticate automatically. Spot needs `GetWebsocketTokenAsync`; futures needs `GetListenKeyAsync`.
- Do not use generic `ApiCredentials`; use `XTCredentials`.
- Do not instantiate clients per request. Reuse clients or use DI.
- Do not block on async calls with `.Result` or `.Wait()`.
- Do not invent `GeneralApi`, `MarginApi`, `WalletApi`, `SubAccountApi`, `UsdFuturesSocketApi`, or `CoinFuturesSocketApi`.
- Do not invent testnet environment names. Source exposes `XTEnvironment.Live` and `CreateCustom`.
- Inspect `XT.Net/Interfaces/Clients/**` before generating methods not listed in the docs.

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/XT.Net/
- AI quick map: `docs/ai-api-map.md`
- LLM index: `llms.txt`
- Full LLM context: `llms-full.txt`
- Claude-facing skill: `CLAUDE.md`
- Examples: `Examples/ai-friendly/`
- Source: https://github.com/JKorf/XT.Net
