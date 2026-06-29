// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same pattern works across XT, Binance, OKX, Bybit, Kraken, and other
// CryptoExchange.Net exchange libraries.
//
// Setup:
//   dotnet add package XT.Net
//   dotnet add package Binance.Net   // optional, for a Binance comparison
//   dotnet add package JK.OKX.Net    // optional, for an OKX comparison

using CryptoExchange.Net.SharedApis;
using XT.Net.Clients;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on supported API surfaces.
// Shared clients implement interfaces like ISpotTickerRestClient and IOrderBookSocketClient.
// Use SharedClient.Discover() when you need runtime capability metadata.

ISpotTickerRestClient xtShared = new XTRestClient().SpotApi.SharedClient;

// To add Binance or OKX, install the package and:
//   ISpotTickerRestClient binanceShared = new BinanceRestClient().SpotApi.SharedClient;
//   ISpotTickerRestClient okxShared     = new OKXRestClient().UnifiedApi.SharedClient;

var btcusdt = new SharedSymbol(TradingMode.Spot, "BTC", "USDT");

await PrintTicker(xtShared, btcusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] ticker response: {result.Data}");
}

// ---- AVAILABLE SHARED CLIENTS IN XT.NET ----
// Spot REST:
//   new XTRestClient().SpotApi.SharedClient
// USDT-M Futures REST:
//   new XTRestClient().UsdtFuturesApi.SharedClient
// Coin-M Futures REST:
//   new XTRestClient().CoinFuturesApi.SharedClient
// Spot Socket:
//   new XTSocketClient().SpotApi.SharedClient
// Futures Socket:
//   new XTSocketClient().FuturesApi.SharedClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
// Shared socket subscriptions return WebSocketResult<UpdateSubscription>.
var xtSocket = new XTSocketClient();
ITickerSocketClient tickerSocket = xtSocket.SpotApi.SharedClient;

var sub = await tickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(btcusdt),
    update => Console.WriteLine($"[{tickerSocket.Exchange}] ticker update: {update.Data}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

// The shared ticker interface does not expose UnsubscribeAsync; keep the concrete client.
await xtSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Arbitrage scanner:       loop over List<ISpotTickerRestClient>
//   Cross-exchange books:    use IOrderBookSocketClient on each exchange
//   Best execution:          use ISpotOrderRestClient on selected exchanges
//   Futures comparison:      use IFuturesTickerRestClient from UsdtFuturesApi.SharedClient
