// 05-error-handling.cs
//
// Demonstrates: HttpResult patterns, retry logic, common XT.Net error scenarios,
// and symbol validation before order placement.
//
// Setup:
//   dotnet add package XT.Net

using CryptoExchange.Net.Objects;
using XT.Net;
using XT.Net.Clients;
using XT.Net.Enums;

var client = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});

// ---- 1. THE BASIC PATTERN ----
// Direct and SharedApis REST methods return HttpResult<T> or HttpResult.
// Direct and SharedApis WebSocket subscriptions return WebSocketResult<UpdateSubscription>.
// .Success is true/false. .Data is valid only when .Success is true.
// .Error contains structured error info when .Success is false.
// .Error.IsTransient hints whether a retry might succeed.

var result = await client.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");

if (result.Success)
{
    Console.WriteLine($"Ticker response: {result.Data.First()}");
}
else
{
    Console.WriteLine($"Code:      {result.Error?.Code}");
    Console.WriteLine($"Message:   {result.Error?.Message}");
    Console.WriteLine($"Type:      {result.Error?.ErrorType}");
    Console.WriteLine($"Transient: {result.Error?.IsTransient}");
}

// ---- 2. SIMPLE RETRY WITH BACKOFF ----
// Retry only on transient errors such as network issues, rate limits, or server overload.
// Do not retry validation errors, bad credentials, or insufficient balance.

async Task<HttpResult<T>> WithRetry<T>(
    Func<Task<HttpResult<T>>> call,
    int maxAttempts = 3)
{
    HttpResult<T> last = default!;
    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        last = await call();
        if (last.Success)
            return last;

        if (last.Error?.IsTransient != true)
            return last;

        await Task.Delay(TimeSpan.FromMilliseconds(250 * Math.Pow(2, attempt)));
    }

    return last;
}

var ticker = await WithRetry(
    () => client.SpotApi.ExchangeData.GetTickersAsync("eth_usdt"));

if (ticker.Success)
    Console.WriteLine($"Retry helper ticker: {ticker.Data.First()}");

// ---- 3. COMMON XT ERROR SCENARIOS ----
//
// Authentication or signature error:
//   API key, secret, permissions, or timestamp/signature setup is wrong.
//   Permanent until configuration changes; do not retry indefinitely.
//
// Rate limit / temporary service errors:
//   Usually transient. Retry with backoff and reuse clients so client-side
//   rate limiting has a chance to work.
//
// Invalid symbol or unsupported market:
//   Permanent for that request. Use GetSymbolsAsync before placing orders or
//   when accepting user-supplied symbols.
//
// Invalid order quantity / price:
//   Permanent until the caller adjusts order size, price, or order type.
//   Query symbol metadata and round to exchange precision rather than using
//   ad hoc string truncation.
//
// Insufficient balance:
//   Permanent for that account state. Surface to the caller.
//
// Private WebSocket stream not receiving events:
//   Spot streams need GetWebsocketTokenAsync; futures streams need GetListenKeyAsync.

// ---- 4. ORDER PLACEMENT WITH SYMBOL VALIDATION ----
var symbols = await client.SpotApi.ExchangeData.GetSymbolsAsync(symbol: "eth_usdt");
if (!symbols.Success)
{
    Console.WriteLine($"Cannot fetch symbol info: {symbols.Error}");
    return;
}

// The exact filter/precision model can evolve; use the source model fields in
// your application to round quantity and price before sending a live order.
Console.WriteLine($"Symbol metadata response: {symbols.Data}");

var order = await client.SpotApi.Trading.PlaceOrderAsync(
    symbol: "eth_usdt",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    timeInForce: TimeInForce.GoodTillCanceled,
    businessType: BusinessType.Spot,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success)
{
    var category = order.Error?.IsTransient == true
        ? "Transient - retry with backoff"
        : "Permanent - surface to user";

    Console.WriteLine($"{category}: {order.Error?.Code} {order.Error?.Message}");
}

// ---- 5. EXCEPTIONS VS ERROR RESULTS ----
// XT.Net returns exchange, rate limit, and network errors via result.Error.
// Exceptions are generally for misconfiguration, disposal, cancellation, or
// programmer errors. Pass CancellationToken with `ct:` when requests need to be cancelable.

// Common variations:
//   With CancellationToken:    pass `ct: cancellationToken` to any method
//   With timeout per request:  options.RequestTimeout = TimeSpan.FromSeconds(10)
//   Polly integration:         use IsTransient as the retry predicate
