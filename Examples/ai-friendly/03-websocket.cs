// Install XT.Net; set XT_API_KEY and XT_API_SECRET for private subscriptions.
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
    var ticker = await socket.SpotApi.SubscribeToTickerUpdatesAsync(
        "eth_usdt", update => Console.WriteLine(update.Data.LastPrice));
    if (!ticker.Success)
    {
        Console.WriteLine(ticker.Error);
        return;
    }
    var futuresTicker = await socket.FuturesApi.SubscribeToTickerUpdatesAsync(
        "ETH_USDT", update => Console.WriteLine(update.Data));
    if (!futuresTicker.Success)
    {
        Console.WriteLine(futuresTicker.Error);
        return;
    }
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
