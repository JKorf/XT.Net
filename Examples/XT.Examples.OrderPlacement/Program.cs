using XT.Net;
using XT.Net.Clients;
using XT.Net.Enums;

const string spotSymbol = "btc_usdt";
const string futuresSymbol = "eth_usdt";

// Replace with valid credentials or order placement will always fail
var apiKey = "KEY";
var apiSecret = "SECRET";

Console.WriteLine("XT.Net order placement example");
Console.WriteLine();
Console.WriteLine("This example can place real orders when valid credentials are configured.");
Console.WriteLine();

var client = new XTRestClient(options =>
{
    options.ApiCredentials = new XTCredentials(apiKey, apiSecret);
});

await PlaceSpotLimitOrderAsync(client);
Console.WriteLine();
await PlaceFuturesLimitOrderExampleAsync(client);

static async Task PlaceSpotLimitOrderAsync(XTRestClient client)
{
    Console.WriteLine($"Placing spot limit buy order for {spotSymbol}...");

    var tickers = await client.SpotApi.ExchangeData.GetTickersAsync(spotSymbol);
    if (!tickers.Success)
    {
        Console.WriteLine($"Failed to get spot ticker: {tickers.Error}");
        return;
    }

    var ticker = tickers.Data.Single();
    if (ticker.LastPrice == null)
    {
        Console.WriteLine("Failed to get spot ticker: response did not include a last price");
        return;
    }

    var safePrice = Math.Round(ticker.LastPrice.Value * 0.95m, 2);
    var order = await client.SpotApi.Trading.PlaceOrderAsync(
        symbol: spotSymbol,
        orderSide: OrderSide.Buy,
        orderType: OrderType.Limit,
        timeInForce: TimeInForce.GoodTillCanceled,
        businessType: BusinessType.Spot,
        quantity: 0.001m,
        price: safePrice);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place spot order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed spot order {order.Data.OrderId}");

    var orderStatus = await client.SpotApi.Trading.GetOrderAsync(order.Data.OrderId);
    if (orderStatus.Success)
        Console.WriteLine($"Spot order status: {orderStatus.Data.OrderStatus}, filled: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query spot order: {orderStatus.Error}");

    var cancel = await client.SpotApi.Trading.CancelOrderAsync(order.Data.OrderId);
    Console.WriteLine(cancel.Success
        ? $"Cancelled spot order {order.Data.OrderId}"
        : $"Failed to cancel spot order: {cancel.Error}");
}

static async Task PlaceFuturesLimitOrderExampleAsync(XTRestClient client)
{
    Console.WriteLine($"Placing USDT futures limit sell order for {futuresSymbol}...");

    var ticker = await client.UsdtFuturesApi.ExchangeData.GetTickerAsync(futuresSymbol);
    if (!ticker.Success)
    {
        Console.WriteLine($"Failed to get futures ticker: {ticker.Error}");
        return;
    }

    var safePrice = Math.Round(ticker.Data.LastPrice * 1.05m, 2);
    var order = await client.UsdtFuturesApi.Trading.PlaceOrderAsync(
        symbol: futuresSymbol,
        orderSide: OrderSide.Sell,
        orderType: OrderType.Limit,
        quantity: 0.01m,
        positionSide: PositionSide.Short,
        price: safePrice,
        timeInForce: TimeInForce.GoodTillCanceled);

    if (!order.Success)
    {
        Console.WriteLine($"Failed to place futures order: {order.Error}");
        return;
    }

    Console.WriteLine($"Placed futures order {order.Data}");

    var orderStatus = await client.UsdtFuturesApi.Trading.GetOrderAsync(order.Data);
    if (orderStatus.Success)
        Console.WriteLine($"Futures order status: {orderStatus.Data.Status}, executed: {orderStatus.Data.QuantityFilled}");
    else
        Console.WriteLine($"Failed to query futures order: {orderStatus.Error}");

    var cancel = await client.UsdtFuturesApi.Trading.CancelOrderAsync(order.Data);
    Console.WriteLine(cancel.Success
        ? $"Cancelled futures order {order.Data}"
        : $"Failed to cancel futures order: {cancel.Error}");
}
