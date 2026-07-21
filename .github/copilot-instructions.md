# Copilot Instructions for XT.Net

This repository is XT.Net, a strongly typed C#/.NET client library for the XT cryptocurrency exchange API. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes XT.Net, follow these conventions.

## Use XT.Net, not raw HTTP

Never generate `HttpClient` calls to XT endpoints. Always use `XTRestClient` or `XTSocketClient` so signing, rate limiting, serialization, and error handling are handled by the library.

## Client Setup

```csharp
using XT.Net;
using XT.Net.Clients;

var restClient = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});
```

For public market data only, credentials are not required: `new XTRestClient()`.

## Result Handling

Methods return `WebCallResult<T>` or `WebCallResult` for REST and `CallResult<T>` for WebSocket subscriptions. Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API Structure

- `restClient.SpotApi.ExchangeData` - spot market data
- `restClient.SpotApi.Account` - spot balances, deposits, withdrawals, transfers, websocket token
- `restClient.SpotApi.Trading` - spot orders and user trades
- `restClient.UsdtFuturesApi.*` - USDT-M futures REST
- `restClient.CoinFuturesApi.*` - Coin-M futures REST
- `socketClient.SpotApi` - spot WebSocket streams
- `socketClient.FuturesApi` - futures WebSocket streams

Private spot socket streams require `restClient.SpotApi.Account.GetWebsocketTokenAsync()`. Private futures socket streams require `restClient.UsdtFuturesApi.Account.GetListenKeyAsync()` or the matching futures REST account listen key.

## Order Placement

Spot orders require `TimeInForce` and `BusinessType`:

```csharp
var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    "eth_usdt", OrderSide.Buy, OrderType.Limit,
    TimeInForce.GoodTillCanceled, BusinessType.Spot,
    quantity: 0.01m, price: 2000m);
```

Futures orders require `PositionSide`:

```csharp
var order = await restClient.UsdtFuturesApi.Trading.PlaceOrderAsync(
    "ETH_USDT", OrderSide.Buy, OrderType.Market, 0.01m, PositionSide.Long);
```

## WebSocket Pattern

Store returned `UpdateSubscription` values and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-Exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces from `.SharedClient`.

After a successful shared symbol query, `ISpotSymbolRestClient.SpotSymbolCatalog` or `IFuturesSymbolRestClient.FuturesSymbolCatalog` provides the cached catalog. Shared symbol results include display names and asset type/subtype metadata, including XT-tagged equities and commodities and recognized stablecoins.

## Avoid

- Raw `HttpClient` XT endpoint calls
- Generic `ApiCredentials` instead of `XTCredentials`
- Synchronous `.Result` / `.Wait()`
- Instantiating clients per request
- Reading `.Data` without checking `.Success`
- `socketClient.UsdtFuturesApi` or `socketClient.CoinFuturesApi`; use `socketClient.FuturesApi`
- Private WebSocket subscriptions without the required token/listen key

## Reference

For detailed patterns and pitfalls see `AGENTS.md`, `llms.txt`, and `llms-full.txt` in the repository root, `docs/ai-api-map.md` for intent-to-method routing, and `Examples/ai-friendly/` for compilable examples.
