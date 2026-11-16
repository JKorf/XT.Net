# Usage examples

Install `XT.Net`. Each C# block is independent and includes its imports. Order examples define functions without invoking them; compiling never submits an order. See [API surfaces](api-surfaces.md) for routing and [operational considerations](safety.md) before account-changing workflows.

## Public setup and results

```csharp
using XT.Net.Clients;

using var client = new XTRestClient();
var result = await client.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
var ticker = result.Data.FirstOrDefault();
if (ticker is not null)
    Console.WriteLine(ticker.LastPrice);
```

## Credentials and account reads

```csharp
using XT.Net;
using XT.Net.Clients;

var key = Environment.GetEnvironmentVariable("XT_API_KEY")
    ?? throw new InvalidOperationException("Missing API key");
var secret = Environment.GetEnvironmentVariable("XT_API_SECRET")
    ?? throw new InvalidOperationException("Missing API secret");
using var client = new XTRestClient(options =>
    options.ApiCredentials = new XTCredentials(key, secret));
var spot = await client.SpotApi.Account.GetBalancesAsync();
if (!spot.Success)
    Console.WriteLine(spot.Error);
else
    Console.WriteLine(spot.Data);
var futures = await client.UsdtFuturesApi.Account.GetBalancesAsync();
if (!futures.Success)
    Console.WriteLine(futures.Error);
else
    Console.WriteLine(futures.Data);
```

## Spot limit-order function

Invoke only with an authenticated client and validated symbol, price, and quantity. For a market buy, use quote spend rather than copying this base-quantity limit-order shape.

```csharp
using XT.Net.Clients;
using XT.Net.Enums;

static async Task SubmitSpotLimitAsync(
    XTRestClient client, string symbol, decimal quantity, decimal price, CancellationToken ct)
{
    var result = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol, OrderSide.Buy, OrderType.Limit, TimeInForce.GoodTillCanceled,
        BusinessType.Spot, quantity: quantity, price: price, ct: ct);
    if (!result.Success)
        Console.WriteLine(result.Error);
    else
        Console.WriteLine($"Submitted order {result.Data.OrderId}");
}
```

## Futures order function

This function deliberately selects USDT-M and does not change leverage or margin configuration. Coin-M uses `CoinFuturesApi`; validate its contract quantity units separately.

```csharp
using XT.Net.Clients;
using XT.Net.Enums;

static async Task SubmitUsdtFuturesLimitAsync(
    XTRestClient client, string symbol, OrderSide side, PositionSide positionSide,
    decimal quantity, decimal price, CancellationToken ct)
{
    var result = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
        symbol, side, OrderType.Limit, quantity, positionSide,
        price: price, timeInForce: TimeInForce.GoodTillCanceled, ct: ct);
    if (!result.Success)
        Console.WriteLine(result.Error);
    else
        Console.WriteLine(result.Data);
}
```

## Public subscription lifecycle

```csharp
using XT.Net.Clients;

using var socket = new XTSocketClient();
var subscription = await socket.SpotApi.SubscribeToTickerUpdatesAsync(
    "eth_usdt", update => Console.WriteLine(update.Data.LastPrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socket.UnsubscribeAsync(subscription.Data);
}
```

## Private token and listen-key subscriptions

These explicit overloads keep the REST account/product that issued each token visible. Credentialed socket overloads without a token/key are also available and manage acquisition internally.

```csharp
using XT.Net;
using XT.Net.Clients;

var key = Environment.GetEnvironmentVariable("XT_API_KEY")
    ?? throw new InvalidOperationException("Missing API key");
var secret = Environment.GetEnvironmentVariable("XT_API_SECRET")
    ?? throw new InvalidOperationException("Missing API secret");
using var rest = new XTRestClient(options =>
    options.ApiCredentials = new XTCredentials(key, secret));
using var socket = new XTSocketClient();
try
{
    var token = await rest.SpotApi.Account.GetWebsocketTokenAsync();
    if (!token.Success)
    {
        Console.WriteLine(token.Error);
        return;
    }
    var orders = await socket.SpotApi.SubscribeToOrderUpdatesAsync(
        token.Data, update => Console.WriteLine(update.Data));
    if (!orders.Success)
    {
        Console.WriteLine(orders.Error);
        return;
    }
    var listenKey = await rest.UsdtFuturesApi.Account.GetListenKeyAsync();
    if (!listenKey.Success)
    {
        Console.WriteLine(listenKey.Error);
        return;
    }
    var positions = await socket.FuturesApi.SubscribeToPositionUpdatesAsync(
        listenKey.Data, update => Console.WriteLine(update.Data));
    if (!positions.Success)
    {
        Console.WriteLine(positions.Error);
        return;
    }
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    // This socket client belongs exclusively to the example, including partial setup.
    await socket.UnsubscribeAllAsync();
}
```

## Shared API V2

```csharp
using CryptoExchange.Net.SharedApis;
using XT.Net.Clients;

using var client = new XTRestClient();
IGetTickerRest ticker = client.SpotApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
    Console.WriteLine($"{ticker.Exchange}: {result.Error}");
else
    Console.WriteLine(result.Data.LastPrice);
```

## DI and runtime selection

The container owns its clients. With multiple exchanges, select the exchange aggregate rather than relying on the last registration of a common capability.

```csharp
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using XT.Net;
using XT.Net.Interfaces.Clients;

var services = new ServiceCollection();
services.AddXT(options => options.Environment = XTEnvironment.Live);
using var provider = services.BuildServiceProvider();
var shared = provider.GetRequiredService<IXTSharedApiClient>();
IGetTickerRest knownSurface = shared.SpotRest;
var match = shared.GetCapability(
    SharedCapabilities.Tickers.GetTicker.Rest, TradingMode.Spot);
if (match is null)
    return;
Console.WriteLine(match.Options);
var result = await match.Capability.GetTickerAsync(
    new GetTickerRequest(new SharedSymbol(TradingMode.Spot, "ETH", "USDT")));
if (!result.Success)
    Console.WriteLine(result.Error);
else
    Console.WriteLine(result.Data.LastPrice);
```

Checkout-only: [standalone programs](../../../../Examples/ai-friendly/README.md) and [complete projects](../../../../Examples/README.md). Build without running to validate usage without exchange operations. Some programs change live account state when run with valid credentials.
