# API surfaces

## Product roots and setup

`XTRestClient` / `IXTRestClient` expose `SpotApi`, `UsdtFuturesApi`, and `CoinFuturesApi`; each has `ExchangeData`, `Account`, `Trading`, and `SharedApi`. `XTSocketClient` / `IXTSocketClient` expose `SpotApi` and the combined `FuturesApi`. There are no separate USDT-M/Coin-M socket roots.

Private access uses `XTCredentials(key, secret)`. `XTEnvironment.Live` is built in; `CreateCustom` configures custom endpoints without providing a built-in sandbox. Spot native examples use `eth_usdt`, futures interfaces use `ETH_USDT`; discover the actual symbol for the selected product.

## Spot REST

Spot source examples use symbols like `eth_usdt`.

| User intent | XT.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get client IP | `client.SpotApi.ExchangeData.GetClientIpAsync()` |
| Get spot symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync()` |
| Get info for one spot symbol | `client.SpotApi.ExchangeData.GetSymbolsAsync(symbol: "eth_usdt")` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("eth_usdt", limit: 100)` |
| Get spot klines/candles | `client.SpotApi.ExchangeData.GetKlinesAsync("eth_usdt", KlineInterval.OneMinute)` |
| Get recent spot trades | `client.SpotApi.ExchangeData.GetRecentTradesAsync("eth_usdt")` |
| Get spot trade history | `client.SpotApi.ExchangeData.GetTradeHistoryAsync("eth_usdt")` |
| Get spot ticker | `client.SpotApi.ExchangeData.GetTickersAsync("eth_usdt")` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot price ticker | `client.SpotApi.ExchangeData.GetPriceTickersAsync("eth_usdt")` |
| Get spot book ticker | `client.SpotApi.ExchangeData.GetBookTickersAsync("eth_usdt")` |
| Get 24h spot ticker | `client.SpotApi.ExchangeData.Get24HTickersAsync("eth_usdt")` |
| Get assets | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get asset network info | `client.SpotApi.ExchangeData.GetAssetNetworksAsync()` |
| Get one spot balance | `client.SpotApi.Account.GetBalanceAsync("usdt")` |
| Get spot balances | `client.SpotApi.Account.GetBalancesAsync()` |
| Get deposit address | `client.SpotApi.Account.GetDepositAddressAsync(asset, network)` |
| Get deposit history | `client.SpotApi.Account.GetDepositHistoryAsync(asset, network)` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(asset, network, address, quantity)` |
| Get withdrawal history | `client.SpotApi.Account.GetWithdrawalHistoryAsync(asset, network)` |
| Transfer between account types | `client.SpotApi.Account.TransferAsync(asset, from, to, quantity, clientId)` |
| Transfer to subaccount | `client.SpotApi.Account.SubAccountTransferAsync(...)` |
| Get spot private socket token | `client.SpotApi.Account.GetWebsocketTokenAsync()` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(...)` |
| Query spot order by id | `client.SpotApi.Trading.GetOrderAsync(orderId)` |
| Query spot order by client id | `client.SpotApi.Trading.GetOrderByClientOrderIdAsync(clientOrderId)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(orderId)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(...)` |
| Get closed spot orders | `client.SpotApi.Trading.GetClosedOrdersAsync(...)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(BusinessType.Spot, symbol: "eth_usdt")` |
| Edit spot order | `client.SpotApi.Trading.EditOrderAsync(orderId, quantity, price)` |
| Get multiple spot orders | `client.SpotApi.Trading.GetOrdersAsync(orderIds)` |
| Cancel multiple spot orders | `client.SpotApi.Trading.CancelOrdersAsync(orderIds)` |
| Get spot user trades | `client.SpotApi.Trading.GetUserTradesAsync(...)` |

## USDT-M Futures REST

Use `CoinFuturesApi` instead of `UsdtFuturesApi` for Coin-M futures. The futures REST interface shape is shared.

| User intent | XT.Net member |
|---|---|
| Get futures server time | `client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get futures client IP | `client.UsdtFuturesApi.ExchangeData.GetClientIpAsync()` |
| Get futures quote assets | `client.UsdtFuturesApi.ExchangeData.GetSymbolAssetsAsync()` |
| Get one futures symbol | `client.UsdtFuturesApi.ExchangeData.GetSymbolAsync("ETH_USDT")` |
| Get futures symbols | `client.UsdtFuturesApi.ExchangeData.GetSymbolsAsync()` |
| Get leverage bracket for symbol | `client.UsdtFuturesApi.ExchangeData.GetLeverageBracketsAsync("ETH_USDT")` |
| Get all leverage brackets | `client.UsdtFuturesApi.ExchangeData.GetLeverageBracketsAsync()` |
| Get futures ticker | `client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH_USDT")` |
| Get all futures tickers | `client.UsdtFuturesApi.ExchangeData.GetTickersAsync()` |
| Get futures recent trades | `client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH_USDT")` |
| Get futures order book | `client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH_USDT")` |
| Get index price | `client.UsdtFuturesApi.ExchangeData.GetIndexPriceAsync("ETH_USDT")` |
| Get all index prices | `client.UsdtFuturesApi.ExchangeData.GetIndexPricesAsync()` |
| Get mark price | `client.UsdtFuturesApi.ExchangeData.GetMarkPriceAsync("ETH_USDT")` |
| Get all mark prices | `client.UsdtFuturesApi.ExchangeData.GetMarkPricesAsync()` |
| Get futures klines | `client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH_USDT", FuturesKlineInterval.OneMinute)` |
| Get futures market info | `client.UsdtFuturesApi.ExchangeData.GetMarketInfoAsync("ETH_USDT")` |
| Get all futures market info | `client.UsdtFuturesApi.ExchangeData.GetMarketInfosAsync()` |
| Get funding rate | `client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH_USDT")` |
| Get futures book ticker | `client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync("ETH_USDT")` |
| Get all futures book tickers | `client.UsdtFuturesApi.ExchangeData.GetBookTickersAsync()` |
| Get funding rate history | `client.UsdtFuturesApi.ExchangeData.GetFundingRateHistoryAsync("ETH_USDT")` |
| Get risk balance | `client.UsdtFuturesApi.ExchangeData.GetRiskBalanceAsync("ETH_USDT")` |
| Get open interest | `client.UsdtFuturesApi.ExchangeData.GetOpenInterestAsync("ETH_USDT")` |
| Get futures symbol info | `client.UsdtFuturesApi.ExchangeData.GetSymbolInfoAsync()` |
| Get futures balances | `client.UsdtFuturesApi.Account.GetBalancesAsync()` |
| Get futures account info | `client.UsdtFuturesApi.Account.GetAccountInfoAsync()` |
| Get futures user asset | `client.UsdtFuturesApi.Account.GetUserAssetAsync("USDT")` |
| Get futures user assets | `client.UsdtFuturesApi.Account.GetUserAssetsAsync()` |
| Get futures account bills | `client.UsdtFuturesApi.Account.GetAccountBillsAsync()` |
| Get funding fee history | `client.UsdtFuturesApi.Account.GetFundingFeeHistoryAsync()` |
| Get futures fee rate | `client.UsdtFuturesApi.Account.GetFeeRateAsync()` |
| Set leverage | `client.UsdtFuturesApi.Account.SetLeverageAsync("ETH_USDT", PositionSide.Long, 5)` |
| Adjust margin | `client.UsdtFuturesApi.Account.AdjustMarginAsync(...)` |
| Get ADL info | `client.UsdtFuturesApi.Account.GetAdlInfoAsync()` |
| Set position type | `client.UsdtFuturesApi.Account.SetPositionTypeAsync(...)` |
| Get futures private socket listen key | `client.UsdtFuturesApi.Account.GetListenKeyAsync()` |
| Place futures order | `client.UsdtFuturesApi.Trading.PlaceOrderAsync(...)` |
| Place multiple futures orders | `client.UsdtFuturesApi.Trading.PlaceMultipleOrdersAsync(orders)` |
| Edit futures order | `client.UsdtFuturesApi.Trading.EditOrderAsync(...)` |
| Get closed futures orders | `client.UsdtFuturesApi.Trading.GetClosedOrdersAsync(...)` |
| Get futures user trades | `client.UsdtFuturesApi.Trading.GetUserTradesAsync(...)` |
| Query futures order | `client.UsdtFuturesApi.Trading.GetOrderAsync(orderId)` |
| Get futures orders | `client.UsdtFuturesApi.Trading.GetOrdersAsync(...)` |
| Cancel futures order | `client.UsdtFuturesApi.Trading.CancelOrderAsync(orderId)` |
| Cancel all futures orders | `client.UsdtFuturesApi.Trading.CancelAllOrdersAsync("ETH_USDT")` |
| Get open futures positions | `client.UsdtFuturesApi.Trading.GetPositionsAsync("ETH_USDT")` |
| Get futures position info | `client.UsdtFuturesApi.Trading.GetPositionsInfoAsync("ETH_USDT")` |
| Close all positions | `client.UsdtFuturesApi.Trading.CloseAllPositionsAsync()` |
| Get margin call info | `client.UsdtFuturesApi.Trading.GetMarginCallInfoAsync("ETH_USDT")` |
| Place trigger order | `client.UsdtFuturesApi.Trading.PlaceTriggerOrderAsync(...)` |
| Cancel trigger order | `client.UsdtFuturesApi.Trading.CancelTriggerOrderAsync(orderId)` |
| Cancel all trigger orders | `client.UsdtFuturesApi.Trading.CancelAllTriggerOrdersAsync("ETH_USDT")` |
| Get trigger orders | `client.UsdtFuturesApi.Trading.GetTriggerOrdersAsync(...)` |
| Get trigger order | `client.UsdtFuturesApi.Trading.GetTriggerOrderAsync(orderId)` |
| Get closed trigger orders | `client.UsdtFuturesApi.Trading.GetClosedTriggerOrdersAsync(...)` |
| Place stop-limit order | `client.UsdtFuturesApi.Trading.PlaceStopLimitOrderAsync(...)` |
| Cancel stop-limit order | `client.UsdtFuturesApi.Trading.CancelStopLimitOrderAsync(orderId)` |
| Get stop-limit orders | `client.UsdtFuturesApi.Trading.GetStopLimitOrdersAsync(...)` |
| Place track order | `client.UsdtFuturesApi.Trading.PlaceTrackOrderAsync(...)` |
| Cancel track order | `client.UsdtFuturesApi.Trading.CancelTrackOrderAsync(orderId)` |
| Get track order | `client.UsdtFuturesApi.Trading.GetTrackOrderAsync(orderId)` |
| Get open track orders | `client.UsdtFuturesApi.Trading.GetOpenTrackOrdersAsync(...)` |
| Get closed track orders | `client.UsdtFuturesApi.Trading.GetClosedTrackOrdersAsync(...)` |

## Coin-M Futures REST

| User intent | XT.Net member |
|---|---|
| Coin-M futures market data | `client.CoinFuturesApi.ExchangeData.*` |
| Coin-M futures account calls | `client.CoinFuturesApi.Account.*` |
| Coin-M futures trading calls | `client.CoinFuturesApi.Trading.*` |
| Coin-M shared REST client | `client.CoinFuturesApi.SharedApi` |

## Spot WebSocket

| User intent | XT.Net member |
|---|---|
| Subscribe spot trade updates | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync("eth_usdt", handler)` |
| Subscribe many spot trade updates | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync(symbols, handler)` |
| Subscribe spot kline updates | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync("eth_usdt", KlineInterval.OneMinute, handler)` |
| Subscribe spot order book updates | `socketClient.SpotApi.SubscribeToOrderBookUpdatesAsync("eth_usdt", depth, handler)` |
| Subscribe spot incremental order book updates | `socketClient.SpotApi.SubscribeToIncrementalOrderBookUpdatesAsync("eth_usdt", handler)` |
| Subscribe spot ticker updates | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", handler)` |
| Subscribe spot balance updates | `socketClient.SpotApi.SubscribeToBalanceUpdatesAsync(token, handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(token, handler)` |
| Subscribe spot user trade updates | `socketClient.SpotApi.SubscribeToUserTradeUpdatesAsync(token, handler)` |

Private spot streams require `token = await restClient.SpotApi.Account.GetWebsocketTokenAsync()`.

## Futures WebSocket

| User intent | XT.Net member |
|---|---|
| Subscribe futures trades | `socketClient.FuturesApi.SubscribeToTradeUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures klines | `socketClient.FuturesApi.SubscribeToKlineUpdatesAsync("ETH_USDT", KlineInterval.OneMinute, handler)` |
| Subscribe futures ticker | `socketClient.FuturesApi.SubscribeToTickerUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures aggregated ticker | `socketClient.FuturesApi.SubscribeToAggregatedTickerUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures index price | `socketClient.FuturesApi.SubscribeToIndexPriceUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures mark price | `socketClient.FuturesApi.SubscribeToMarkPriceUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures incremental order book | `socketClient.FuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync("ETH_USDT", updateInterval, handler)` |
| Subscribe futures order book | `socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync("ETH_USDT", depth, updateInterval, handler)` |
| Subscribe futures funding rate | `socketClient.FuturesApi.SubscribeToFundingRateUpdatesAsync("ETH_USDT", handler)` |
| Subscribe futures balance updates | `socketClient.FuturesApi.SubscribeToBalancesUpdatesAsync(listenKey, handler)` |
| Subscribe futures position updates | `socketClient.FuturesApi.SubscribeToPositionUpdatesAsync(listenKey, handler)` |
| Subscribe futures order updates | `socketClient.FuturesApi.SubscribeToOrderUpdatesAsync(listenKey, handler)` |
| Subscribe futures user trade updates | `socketClient.FuturesApi.SubscribeToUserTradeUpdatesAsync(listenKey, handler)` |
| Subscribe futures notification updates | `socketClient.FuturesApi.SubscribeToNotificationUpdatesAsync(listenKey, handler)` |

Private futures streams require `listenKey = await restClient.UsdtFuturesApi.Account.GetListenKeyAsync()`.


## Shared API V2

`IXTSharedApiClient` implements `ISharedApiClientBase`. `SpotRest` exposes `IXTRestClientSpotSharedApi`; `UsdtFuturesRest` and `CoinFuturesRest` expose separate implementations of `IXTRestClientFuturesSharedApi`. `SpotSocket` and `FuturesSocket` expose the corresponding socket contracts:

| Surface type | Supported capabilities |
| --- | --- |
| `IXTRestClientSpotSharedApi` | `IGetAssetRest`, `IGetAllAssetsRest`, `IGetBalancesRest`, `IGetDepositAddressesRest`, `IGetDepositHistoryRest`, `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetWithdrawalHistoryRest`, `IWithdrawRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetSpotSymbolsRest`, `IPlaceSpotOrderRest`, `IGetSpotOrderRest`, `IGetOpenSpotOrdersRest`, `IGetClosedSpotOrdersRest`, `IGetSpotOrderTradesRest`, `IGetSpotUserTradeHistoryRest`, `ICancelSpotOrderRest`, `IGetFeesRest`, `IGetBookTickerRest`, `ITransferRest`, `IEditSpotOrderRest`, `ICancelAllSpotOrdersRest`, `ICancelAllSpotSymbolOrdersRest` |
| `IXTRestClientFuturesSharedApi` | `IGetBalancesRest`, `IGetKlinesRest`, `IGetOrderBookRest`, `IGetRecentTradesRest`, `IGetFundingRateHistoryRest`, `IGetFuturesSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, `IGetLeverageRest`, `ISetLeverageRest`, `IGetOpenInterestRest`, `IPlaceFuturesOrderRest`, `IGetFuturesOrderRest`, `IGetOpenFuturesOrdersRest`, `IGetClosedFuturesOrdersRest`, `IGetFuturesOrderTradesRest`, `IGetFuturesUserTradeHistoryRest`, `ICancelFuturesOrderRest`, `IGetPositionsRest`, `IGetFeesRest`, `IPlaceFuturesTriggerOrderRest`, `IGetFuturesTriggerOrderRest`, `ICancelFuturesTriggerOrderRest`, `ISetFuturesTpSlRest`, `ICancelFuturesTpSlRest`, `IGetBookTickerRest`, `IGetMarkPriceRest`, `IGetAllMarkPricesRest`, `IGetIndexPriceRest`, `IGetAllIndexPricesRest`, `IEditFuturesOrderRest`, `ICancelAllFuturesOrdersRest`, `ICancelAllFuturesSymbolOrdersRest`, `IGetLedgerRest`, `IGetUserFundingHistoryRest`, `IGetLeverageTiersRest`, `IGetFundingInfoRest`, `IPlaceMultipleFuturesOrders` |
| `IXTSocketClientSpotSharedApi` | `ISubscribeBalancesSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeUserTradesSocket`, `ISubscribeSpotOrdersSocket` |
| `IXTSocketClientFuturesSharedApi` | `ISubscribeBalancesSocket`, `ISubscribeKlinesSocket`, `ISubscribeOrderBookSocket`, `ISubscribeTickerSocket`, `ISubscribeTradesSocket`, `ISubscribeUserTradesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribePositionsSocket`, `ISubscribeMarkPriceSocket`, `ISubscribeIndexPriceSocket` |

Use the typed `.SharedApi` or an aggregate property for a known operation. For runtime selection, use `GetCapability(SharedCapabilities.Tickers.GetTicker.Rest, TradingMode.Spot)` or `GetCapabilities(...)` for all matches. Choose `TradingMode.PerpetualLinear` for USDT-M and `PerpetualInverse` for Coin-M. Handle a nullable `SharedCapabilityResolution<T>` and inspect `Capability` and `Options`. `Discover()` supplies summary metadata; `PreferredTransport` affects generic selection while `.Rest` requires REST.

Capability presence does not guarantee every request property is supported. Inspect trading modes, request/exchange parameter rules, limits, and notes. Native APIs retain XT-specific trigger, stop-limit, track-order, account, and token/listen-key workflows. V1 `.SharedClient` remains for compatibility; listen keys are lifecycle infrastructure, not V2 capabilities.

## Symbols, catalogs, and quantities

Portable symbols use base/quote components in `SharedSymbol`. Successful `GetSpotSymbolsAsync`/`GetFuturesSymbolsAsync` calls populate `SpotSymbolCatalog`/`FuturesSymbolCatalog` for the selected API/environment, apply `GetSymbolsRequest` filters, and return display names and asset classifications. XT tags map to crypto, stablecoin, equity, and commodity metadata where available; missing metadata stays unknown.

Shared request quantities use `SharedQuantity.Base`, `.Quote`, or `.Contracts` only where the chosen capability supports the unit. Responses use `SharedOrderQuantity`, whose base/quote/contracts quantities are nullable; absent units are not zero. Ticker volume uses `Volumes`, and book-ticker quantities use `BestBidQuantities`/`BestAskQuantities`. Inspect `SharedOrderBook.QuantityType` before interpreting depth. Native spot market buys use `quoteQuantity` for spend; market sells use base `quantity`. Futures quantity must respect the selected contract's unit and size.

## Private streams and services

Spot token overloads take `GetWebsocketTokenAsync().Data`; futures listen-key overloads take the key from the matching futures REST account. Credentialed socket overloads without those arguments acquire/manage tokens internally; do not infer that an unconfigured client is authenticated. Keep account and product context with each token/key, and use the library's lifecycle facilities for long-running streams rather than treating keys as permanent.

`services.AddXT(options => ...)` and `services.AddXT(configuration.GetSection("XT"))` register native clients, V2 capabilities, the exchange aggregate, `IXTOrderBookFactory`, `IXTTrackerFactory`, and `IXTUserClientProvider`. Select the exchange aggregate when multiple exchanges are registered. The book factory exposes `Spot`, `UsdtFutures`, `CoinFutures`, `Create(SharedSymbol, ...)`, and native product creation methods. Check startup success and stop/dispose owned books and trackers. The tracker factory supports spot and USDT-futures user-data trackers; Coin-futures user tracker methods are not active in the interface. The user client provider caches credentialed clients by user identifier.

Checkout-only: [public contracts](../../../../XT.Net/Interfaces/Clients), [Shared API adapters](../../../../XT.Net/Clients), and [DI registration](../../../../XT.Net/ExtensionMethods).
