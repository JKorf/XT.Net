// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions - public spot ticker, spot order book,
// futures ticker, authenticated spot and futures stream setup, and teardown.
//
// Setup:
//   dotnet add package XT.Net

using XT.Net;
using XT.Net.Clients;
using XT.Net.Enums;

// ---- 1. PUBLIC SOCKET CLIENT ----
var socketClient = new XTSocketClient();

var tickerSub = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "eth_usdt",
    update => Console.WriteLine($"Spot ticker update: {update.Data}"));

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe spot ticker: {tickerSub.Error}");
    return;
}

var bookSub = await socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync(
    symbol: "eth_usdt",
    depth: 20,
    onMessage: update => Console.WriteLine($"Spot book update: {update.Data}"));

if (!bookSub.Success)
{
    Console.WriteLine($"Failed to subscribe spot order book: {bookSub.Error}");
    await socketClient.UnsubscribeAsync(tickerSub.Data);
    return;
}

var futuresTickerSub = await socketClient.FuturesApi.SubscribeToTickerUpdatesAsync(
    "ETH_USDT",
    update => Console.WriteLine($"Futures ticker update: {update.Data}"));

if (!futuresTickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe futures ticker: {futuresTickerSub.Error}");
    await socketClient.UnsubscribeAsync(tickerSub.Data);
    await socketClient.UnsubscribeAsync(bookSub.Data);
    return;
}

// ---- 2. PRIVATE SPOT STREAMS NEED A WEBSOCKET TOKEN ----
var restClient = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});

var token = await restClient.SpotApi.Account.GetWebsocketTokenAsync();
if (token.Success)
{
    var orderSub = await socketClient.SpotApi.SubscribeToOrderUpdatesAsync(
        token.Data,
        update => Console.WriteLine($"Spot order update: {update.Data}"));

    if (orderSub.Success)
        await socketClient.UnsubscribeAsync(orderSub.Data);
}

// ---- 3. PRIVATE FUTURES STREAMS NEED A LISTEN KEY ----
var listenKey = await restClient.UsdtFuturesApi.Account.GetListenKeyAsync();
if (listenKey.Success)
{
    var positionSub = await socketClient.FuturesApi.SubscribeToPositionUpdatesAsync(
        listenKey.Data,
        update => Console.WriteLine($"Futures position update: {update.Data}"));

    if (positionSub.Success)
        await socketClient.UnsubscribeAsync(positionSub.Data);
}

// ---- 4. TEARDOWN ----
await socketClient.UnsubscribeAsync(tickerSub.Data);
await socketClient.UnsubscribeAsync(bookSub.Data);
await socketClient.UnsubscribeAsync(futuresTickerSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   Spot klines:             socketClient.SpotApi.SubscribeToKlineUpdatesAsync("eth_usdt", KlineInterval.OneMinute, ...)
//   Spot trades:             socketClient.SpotApi.SubscribeToTradeUpdatesAsync("eth_usdt", ...)
//   Futures klines:          socketClient.FuturesApi.SubscribeToKlineUpdatesAsync("ETH_USDT", KlineInterval.OneMinute, ...)
//   Futures order updates:   socketClient.FuturesApi.SubscribeToOrderUpdatesAsync(listenKey, ...)
//   Futures user trades:     socketClient.FuturesApi.SubscribeToUserTradeUpdatesAsync(listenKey, ...)
