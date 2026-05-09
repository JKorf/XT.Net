// 01-spot-quickstart.cs
//
// Demonstrates: client setup, public market data, authenticated balances,
// limit order placement, order status check, and cancellation.
//
// Setup:
//   dotnet new console -n XTSpotQuickstart && cd XTSpotQuickstart
//   dotnet add package XT.Net
//   Copy this file content into Program.cs
//   Substitute API_KEY / API_SECRET below
//   dotnet run

using XT.Net;
using XT.Net.Clients;
using XT.Net.Enums;

// ---- 1. PUBLIC CLIENT (no credentials needed for market data) ----
// Reuse this client across the application; do not create one per request.
var publicClient = new XTRestClient();

var tickers = await publicClient.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
if (!tickers.Success)
{
    Console.WriteLine($"Failed to get ticker: {tickers.Error}");
    return;
}

var ticker = tickers.Data.First();
Console.WriteLine($"Ticker received: {ticker}");

// ---- 2. AUTHENTICATED CLIENT (for account / trading) ----
var tradingClient = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials("API_KEY", "API_SECRET");
});

var balances = await tradingClient.SpotApi.Account.GetBalancesAsync();
if (!balances.Success)
{
    Console.WriteLine($"Failed to get balances: {balances.Error}");
    return;
}

Console.WriteLine($"Balances response: {balances.Data}");

// ---- 3. PLACE A LIMIT BUY ORDER ----
// Spot orders require TimeInForce and BusinessType. For XT spot market buys,
// use quoteQuantity instead of quantity.
var order = await tradingClient.SpotApi.Trading.PlaceOrderAsync(
    symbol: "eth_usdt",
    orderSide: OrderSide.Buy,
    orderType: OrderType.Limit,
    timeInForce: TimeInForce.GoodTillCanceled,
    businessType: BusinessType.Spot,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success)
{
    Console.WriteLine($"Failed to place order: {order.Error}");
    return;
}

Console.WriteLine($"Placed spot order id: {order.Data.OrderId}");

// ---- 4. CHECK ORDER STATUS ----
var status = await tradingClient.SpotApi.Trading.GetOrderAsync(order.Data.OrderId);
if (status.Success)
    Console.WriteLine($"Order status response: {status.Data}");

// ---- 5. CANCEL THE ORDER (cleanup for this example) ----
var cancel = await tradingClient.SpotApi.Trading.CancelOrderAsync(order.Data.OrderId);
if (cancel.Success)
    Console.WriteLine($"Cancelled order id: {order.Data.OrderId}");

// Common variations:
//   Market buy:        OrderType.Market with quoteQuantity: 25m and no quantity
//   Market sell:       OrderType.Market with quantity: 0.01m
//   Margin account:    BusinessType.Leverage where the endpoint supports it
//   Open orders:       tradingClient.SpotApi.Trading.GetOpenOrdersAsync("eth_usdt")
//   User trades:       tradingClient.SpotApi.Trading.GetUserTradesAsync(symbol: "eth_usdt")
