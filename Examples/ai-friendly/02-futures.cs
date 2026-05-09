// 02-futures.cs
//
// Demonstrates: XT USDT-M futures - set leverage, place a market order,
// retrieve positions, and close positions.
//
// Setup:
//   dotnet add package XT.Net
//   Substitute API_KEY / API_SECRET. The API key must have futures permission.

using XT.Net;
using XT.Net.Clients;
using XT.Net.Enums;

var client = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});

const string symbol = "ETH_USDT";

// ---- 1. SET LEVERAGE ----
var leverage = await client.UsdtFuturesApi.Account.SetLeverageAsync(
    symbol,
    PositionSide.Long,
    leverage: 5);

if (!leverage.Success)
{
    Console.WriteLine($"Failed to set leverage: {leverage.Error}");
    return;
}

Console.WriteLine($"Leverage set for {symbol}");

// ---- 2. PLACE MARKET ORDER (open long position) ----
var openOrder = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
    symbol: symbol,
    orderSide: OrderSide.Buy,
    orderType: OrderType.Market,
    quantity: 0.01m,
    positionSide: PositionSide.Long);

if (!openOrder.Success)
{
    Console.WriteLine($"Failed to open position: {openOrder.Error}");
    return;
}

Console.WriteLine($"Opened futures position via order id: {openOrder.Data}");

// ---- 3. GET CURRENT POSITIONS ----
var positions = await client.UsdtFuturesApi.Trading.GetPositionsAsync(symbol);
if (!positions.Success)
{
    Console.WriteLine($"Failed to get positions: {positions.Error}");
    return;
}

foreach (var position in positions.Data)
    Console.WriteLine($"Position response: {position}");

// ---- 4. CLOSE ALL POSITIONS ----
// Use with care in real applications; this closes all open futures positions
// visible to this API account.
var closeAll = await client.UsdtFuturesApi.Trading.CloseAllPositionsAsync();
if (closeAll.Success)
    Console.WriteLine("Close-all positions request accepted.");

// Common variations:
//   Coin-M futures:         client.CoinFuturesApi.Trading.PlaceOrderAsync(...)
//   Limit order:            OrderType.Limit, add price and TimeInForce
//   Short position:         OrderSide.Sell with PositionSide.Short
//   Position info:          client.UsdtFuturesApi.Trading.GetPositionsInfoAsync(symbol)
//   Trigger order:          client.UsdtFuturesApi.Trading.PlaceTriggerOrderAsync(...)
