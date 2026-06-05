
using XT.Net.Clients;

// REST
var restClient = new XTRestClient();
var ticker = await restClient.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
if (!ticker.Success)
{
    Console.WriteLine($"Failed to get ticker: {ticker.Error}");
    return;
}

Console.WriteLine($"Rest client ticker price for eth_usdt: {ticker.Data.Single().LastPrice}");

Console.WriteLine();
Console.WriteLine("Press enter to start websocket subscription");
Console.ReadLine();

// Websocket
var socketClient = new XTSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", update =>
{
    Console.WriteLine($"Websocket client ticker price for eth_usdt: {update.Data.LastPrice}");
});

if (!subscription.Success)
{
    Console.WriteLine($"Failed to subscribe to ticker updates: {subscription.Error}");
    return;
}

Console.ReadLine();
