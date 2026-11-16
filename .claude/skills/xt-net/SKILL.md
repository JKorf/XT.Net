---
name: xt-net
description: Build C#/.NET XT integrations with XT.Net, including spot and USDT-M/Coin-M futures REST, public and private WebSocket streams, accounts, orders, positions, token/listen-key workflows, and Shared API V2 capabilities.
---

# XT.Net

```shell
dotnet add package XT.Net
```

Use `XTRestClient` and `XTSocketClient` from `XT.Net.Clients`. REST roots are `SpotApi`, `UsdtFuturesApi`, and `CoinFuturesApi`, each with `ExchangeData`, `Account`, `Trading`, and `SharedApi`. Socket roots are `SpotApi` and the combined `FuturesApi`, each with `SharedApi`.

Public data needs no credentials. Private access uses `XTCredentials(key, secret)` from `XT.Net`, without a passphrase. Spot native examples use `eth_usdt`; futures interface examples use `ETH_USDT`. Discover symbols in the selected product rather than silently changing casing. The built-in environment is `XTEnvironment.Live`; custom endpoints do not imply testnet support.

Check `Success` before `Data`. REST returns `HttpResult<T>`/`HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and some shared helpers return `ExchangeCallResult<T>`. Reuse clients, retain successful subscriptions, and unsubscribe before disposing their owning socket client. Respect DI ownership.

Spot placement requires `TimeInForce` and `BusinessType`; futures placement requires `PositionSide`. Distinguish base quantity, quote spend, and futures contract units. Private streams require a spot token or futures listen key; credentialed subscription overloads can acquire/manage these internally. See the routing reference for product-specific details.

Use narrow Shared API V2 capabilities through `.SharedApi` for portable code. `.SharedClient` is V1 compatibility. `IXTSharedApiClient` exposes `SpotRest`, `UsdtFuturesRest`, `CoinFuturesRest`, `SpotSocket`, and `FuturesSocket`. Dynamic lookup can return `null`; inspect trading modes and parameter options. Keep native/shared models separate. No additional skill is required.

## References

- [API surfaces](references/api-surfaces.md): native routes, private-stream prerequisites, actual V2 capabilities, catalogs, quantities, and factories.
- [Usage](references/usage.md): setup, credentials, account reads, explicit order functions, subscriptions, Shared API, and DI.
- [Operational considerations](references/safety.md): position scope, batch outcomes, ambiguous submissions, fund movement, and token handling.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/README.md). The references above remain usable in the packaged skill.
