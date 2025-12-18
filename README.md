# ![XT.Net](https://raw.githubusercontent.com/JKorf/XT.Net/main/XT.Net/Icon/icon.png) XT.Net  

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/XT.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/XT.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/XT.Net?style=for-the-badge)

XT.Net is a client library for accessing the [XT REST and Websocket API](https://doc.xt.com/). 

## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* High performance
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Support for managing different accounts
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library
* Native AOT support

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility, as well as the latest dotnet versions to use the latest framework features.

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/XT.net.svg?style=for-the-badge)](https://www.nuget.org/packages/XT.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/XT.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/XT.Net)

	dotnet add package XT.Net
	
### GitHub packages
XT.Net is available on [GitHub packages](https://github.com/JKorf/XT.Net/pkgs/nuget/XT.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/XT.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/XT.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/XT.Net/releases).

## How to use
* REST Endpoints
	```csharp
	// Get the ETH/USDT ticker via rest request
	var restClient = new XTRestClient();
	var tickerResult = await restClient.SpotApi.ExchangeData.GetTickersAsync("eth_usdt");
	var lastPrice = tickerResult.Data.Single().LastPrice;
	```
* Websocket streams
	```csharp
	// Subscribe to ETH/USDT ticker updates via the websocket API
	var socketClient = new XTSocketClient();
	var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("eth_usdt", (update) => 
	{
	  var lastPrice = update.Data.LastPrice;
	});
	```

For information on the clients, dependency injection, response processing and more see the [documentation](https://cryptoexchange.jkorf.dev?library=XT.Net), or have a look at the examples [here](https://github.com/JKorf/XT.Net/tree/main/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
XT.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://cryptoexchange.jkorf.dev/client-libs/shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Aster|[JKorf/Aster.Net](https://github.com/JKorf/Aster.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Aster.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Aster.Net)|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|BloFin|[JKorf/BloFin.Net](https://github.com/JKorf/BloFin.Net)|[![Nuget version](https://img.shields.io/nuget/v/BloFin.net.svg?style=flat-square)](https://www.nuget.org/packages/BloFin.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|CoinW|[JKorf/CoinW.Net](https://github.com/JKorf/CoinW.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinW.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinW.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|DeepCoin|[JKorf/DeepCoin.Net](https://github.com/JKorf/DeepCoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/DeepCoin.net.svg?style=flat-square)](https://www.nuget.org/packages/DeepCoin.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HTX|[JKorf/HTX.Net](https://github.com/JKorf/HTX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.HTX.net.svg?style=flat-square)](https://www.nuget.org/packages/Jkorf.HTX.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|Toobit|[JKorf/Toobit.Net](https://github.com/JKorf/Toobit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Toobit.net.svg?style=flat-square)](https://www.nuget.org/packages/Toobit.Net)|
|Upbit|[JKorf/Upbit.Net](https://github.com/JKorf/Upbit.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Upbit.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Upbit.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|

When using multiple of these API's the [CryptoClients.Net](https://github.com/JKorf/CryptoClients.Net) package can be used instead which combines this and the other packages and allows easy access to all exchange API's.

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). For discussion and/or questions around the CryptoExchange.Net and implementation libraries, feel free to join.

## Supported functionality

### Spot REST
|API|Supported|Location|
|--|--:|--|
|Market|✓|`restClient.SpotApi.ExchangeData`|
|Order|✓|`restClient.SpotApi.Trading`|
|Trade|✓|`restClient.SpotApi.Trading`|
|Balance|✓|`restClient.SpotApi.Account`|
|Deposit/Withdrawal|✓|`restClient.SpotApi.Account`|
|Transfer|✓|`restClient.SpotApi.Account`|

### Spot Websocket
|API|Supported|Location|
|--|--:|--|
|Public|✓|`socketClient.SpotApi`|
|Private|✓|`socketClient.SpotApi`|

### USDT-M Futures REST
|API|Supported|Location|
|--|--:|--|
|Market data|✓|`restClient.UsdtFuturesApi.ExchangeData`|
|Quote collection|✓|`restClient.UsdtFuturesApi.ExchangeData`|
|Order|✓|`restClient.UsdtFuturesApi.Trading`|
|Entrust|✓|`restClient.UsdtFuturesApi.Trading`|
|User|✓|`restClient.UsdtFuturesApi.Account` / `restClient.UsdtFuturesApi.Trading`|

### COIN-M Futures REST
|API|Supported|Location|
|--|--:|--|
|Market data|✓|`restClient.CoinFuturesApi.ExchangeData`|
|Quote collection|✓|`restClient.CoinFuturesApi.ExchangeData`|
|Order|✓|`restClient.CoinFuturesApi.Trading`|
|Entrust|✓|`restClient.CoinFuturesApi.Trading`|
|User|✓|`restClient.CoinFuturesApi.Account` / `restClient.CoinFuturesApi.Trading`|

### Futures Websocket
|API|Supported|Location|
|--|--:|--|
|Public|✓|`socketClient.FuturesApi`|
|Private|✓|`socketClient.FuturesApi`|

### Margin
|API|Supported|Location|
|--|--:|--|
|*|X||

### Copy Trading
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://www.xt.com/en/accounts/register?ref=1HRM5J)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd 

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 3.0.1 - 18 Dec 2025
    * Fixed message routing for UsdtFuturesApi order book subscription
    * Fixed deserialization error on some endpoints when error is returned

* Version 3.0.0 - 16 Dec 2025
    * Added Net10.0 target framework
    * Updated CryptoExchange.Net version to 10.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/ for full release notes
    * Improved performance across the board, biggest gains in websocket message processing
    * Updated REST message response handling
    * Updated WebSocket message handling
    * Added UseUpdatedDeserialization socket client options to toggle by new and old message handling
    * Added SocketIndividualSubscriptionCombineTarget socket client option
    * Updated Shared API's subscription update types from ExchangeEvent to DataEvent

* Version 2.11.0 - 11 Nov 2025
    * Updated CryptoExchange.Net version to 9.13.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fixed deserialization issue if API returns page with null values

* Version 2.10.0 - 03 Nov 2025
    * Updated CryptoExchange.Net to version 9.12.0
    * Added support for using SharedSymbol.UsdOrStable in Shared APIs
    * Fixed exception when initial trade snapshot has no items in TradeTracker
    * Removed some unhelpful verbose logs

* Version 2.9.0 - 16 Oct 2025
    * Updated CryptoExchange.Net version to 9.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClientOrderId mapping on SharedUserTrade models
    * Added ITransferRestClient.TransferAsync implementation

* Version 2.8.0 - 30 Sep 2025
    * Updated CryptoExchange.Net version to 9.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ITrackerFactory to TrackerFactory implementation
    * Added ContractAddress mapping in Shared IAssetClient implementation

* Version 2.7.0 - 01 Sep 2025
    * Updated CryptoExchange.Net version to 9.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * HTTP REST requests will now use HTTP version 2.0 by default

* Version 2.6.0 - 25 Aug 2025
    * Updated CryptoExchange.Net version to 9.6.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added ClearUserClients method to user client provider

* Version 2.5.1 - 21 Aug 2025
    * Added websocket error check for invalid listen key

* Version 2.5.0 - 20 Aug 2025
    * Updated CryptoExchange.Net to version 9.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added improved error parsing
    * Updated rest request sending too prevent duplicate parameter serialization
    * Fixed error responses not correctly getting logged as error

* Version 2.4.0 - 04 Aug 2025
    * Updated CryptoExchange.Net to version 9.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for multi-symbol Shared socket subscriptions

* Version 2.3.1 - 25 Jul 2025
    * Fixed serialization issue in restClient.SpotApi.Trading.CancelOrdersAsync

* Version 2.3.0 - 23 Jul 2025
    * Updated CryptoExchange.Net to version 9.3.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Updated websocket message matching
    * Fixed culture issue in symbol serialization

* Version 2.2.1 - 16 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fixed issue with websocket ping response parsing

* Version 2.2.0 - 15 Jul 2025
    * Updated CryptoExchange.Net to version 9.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/

* Version 2.1.0 - 02 Jun 2025
    * Updated CryptoExchange.Net to version 9.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added (I)XTUserClientProvider allowing for easy client management when handling multiple users
    * Fixed framework check for setting IsAotCompatible project flag

* Version 2.0.0 - 13 May 2025
    * Updated CryptoExchange.Net to version 9.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to FuturesApi and Spot Shared clients
    * Added IBookTickerRestClient implementation to FuturesApi and SpotApi Shared clients
    * Added IFuturesTriggerOrderRestClient implementation to FuturesApi Shared clients
    * Added IFuturesTpSlRestClient implementation to FuturesApi Shared clients
    * Added takeProfitPrice, stopLossPrice parameter support to V4Api PlaceFuturesOrderAsync
    * Added TakeProfitPrice, StopLossPrice to SharedFuturesOrder model
    * Added TakeProfitPrice, StopLossPrice to SharedPosition model
    * Added QuoteVolume property mapping to SharedSpotTicker model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added conversion of symbol to lowercase in various places
    * Added All property to retrieve all available environment on XTEnvironment
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Fixed some typos
    * Fixed incorrect DataTradeMode on certain Shared interface responses
    * Fixed Futures CancelAllTrackOrdersAsync endpoint
    * Fixed deserialization error in XTOrder model

* Version 2.0.0-beta4 - 01 May 2025
    * Updated CryptoExchange.Net version to 9.0.0-beta5
    * Added property to retrieve all available API environments

* Version 2.0.0-beta3 - 25 Apr 2025
    * Fixed deserialization issue in XTOrder response model

* Version 2.0.0-beta2 - 23 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta2
    * Added Shared spot ticker QuoteVolume mapping
    * Fixed incorrect DataTradeMode on responses
    * Fixed Shared GetSpotSymbolsAsync not updating ExchangeSymbolCache

* Version 2.0.0-beta1 - 22 Apr 2025
    * Updated CryptoExchange.Net to version 9.0.0-beta1, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for Native AOT compilation
    * Added RateLimitUpdated event
    * Added SharedSymbol response property to all Shared interfaces response models returning a symbol name
    * Added GenerateClientOrderId method to FuturesApi and Spot Shared clients
    * Added IBookTickerRestClient implementation to FuturesApi and SpotApi Shared clients
    * Added IFuturesTriggerOrderRestClient implementation to FuturesApi Shared clients
    * Added IFuturesTpSlRestClient implementation to FuturesApi Shared clients
    * Added takeProfitPrice, stopLossPrice parameter support to V4Api PlaceFuturesOrderAsync
    * Added TakeProfitPrice, StopLossPrice to SharedFuturesOrder model
    * Added TakeProfitPrice, StopLossPrice to SharedPosition model
    * Added OptionalExchangeParameters and Supported properties to EndpointOptions
    * Added conversion of symbol to lowercase in various places
    * Refactored Shared clients quantity parameters and responses to use SharedQuantity
    * Updated all IEnumerable response and model types to array response types
    * Removed Newtonsoft.Json dependency
    * Fixed some typos
    * Fixed Futures CancelAllTrackOrdersAsync endpoint

* Version 1.2.1 - 03 Mar 2025
    * Fix for balance update deserialization error

* Version 1.2.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Added MinDepositQuantity, DepositConfirmations and WithdrawPrecision properties to restClient.SpotApi.ExchangeData.GetAssetNetworksAsync response model
    * Fix Mono runtime exception on rest client construction using DI

* Version 1.1.3 - 13 Jan 2025
    * Various fixes in UsdtFutures trigger/track/stop limit order endpoints

* Version 1.1.2 - 08 Jan 2025
    * Fix for restClient.UsdtFuturesApi.ExchangeData.GetSymbolInfoAsync deserialization

* Version 1.1.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to XTExchange class

* Version 1.1.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Improved websocket disconnect detection

* Version 1.0.2 - 20 Dec 2024
    * Added client reference

* Version 1.0.1 - 05 Dec 2024
    * Fixed change percentage response in Shared spot symbol response not being actual percentage

* Version 1.0.0 - 04 Dec 2024
    * Initial version

