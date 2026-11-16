# Operational considerations

## Account, product, and credentials

Private REST and socket access requires the key/secret for the intended account and permissions. Keep credentials, tokens, listen keys, addresses, and identifiers out of source and logs. Preserve product/account identity when selecting user clients or passing a token. The built-in live environment is production; custom endpoints do not establish sandbox support.

## Orders and positions

Spot placement requires `TimeInForce` and `BusinessType`. `BusinessType.Leverage` can create margin exposure. Market-buy quote spend differs from market-sell base quantity. Validate product, trading status, precision, minimum quantity/value, fees, and balance before submitting. A rounded limit price does not validate these constraints and can execute immediately.

Futures use the intended USDT-M or Coin-M REST account, explicit `PositionSide`, and the correct quantity/contract unit. Leverage, position type, and margin changes affect exposure/configuration beyond a single order. Trigger, stop-limit, track, and TP/SL workflows require explicit trigger semantics and current-position validation. `CloseAllPositionsAsync()` affects every open position in its product/account scope; do not substitute it for a single-position close.

## Partial and ambiguous outcomes

Inspect both the outer result and any per-item errors returned by a batch operation. Preserve successful items when reporting partial failure; an acknowledgement does not establish that all orders filled. Cancel-all and batch cancellation can affect many orders. Cancellation races with fills, so reconcile order, fill, and position state after acknowledgement.

A timeout, disconnect, or cancellation after submission can leave an order, transfer, or withdrawal accepted remotely. Query order/account/history state and use supported client identifiers for correlation before repeating a mutation. Neither `Error.IsTransient` nor a client identifier guarantees safe resubmission. Bounded public-read retries with cancellation and backoff are a different decision.

## Funds and stream state

Withdrawals, transfers, and subaccount transfers move funds. Validate asset, network, address, memo/tag, amount, fee, account direction, and unique identifiers for the intended operation. History and balances are separate reads.

Spot tokens and futures listen keys have lifecycle and account/product context. Private subscription setup success does not guarantee uninterrupted current state; handle interruption/reconnection and reconcile when missed updates matter. Keep callbacks short, avoid logging account details indiscriminately, and close subscriptions before disposing owned clients.
