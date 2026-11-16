# Standalone examples

Each file is an independent top-level console program. Copy one into `Program.cs` at a time.

| File | Purpose |
| --- | --- |
| [01-spot-quickstart.cs](01-spot-quickstart.cs) | Public data, balances, and spot orders |
| [02-futures.cs](02-futures.cs) | Futures configuration, orders, and positions |
| [03-websocket.cs](03-websocket.cs) | Public streams, explicit private tokens/listen keys, and cleanup |
| [04-multi-exchange.cs](04-multi-exchange.cs) | Shared API V2 REST and socket market data |
| [05-error-handling.cs](05-error-handling.cs) | Result handling and bounded public-read retries |

```shell
dotnet new console -n MyXTApp
cd MyXTApp
dotnet add package XT.Net
# Copy ONE example into Program.cs
dotnet build
```

Compilation performs no exchange operations. Running trading examples with valid credentials changes live account state; these are not sandbox programs. The socket example requires `XT_API_KEY` and `XT_API_SECRET`. Other authenticated walkthroughs use placeholders that require explicit configuration.

See the [consumer skill](../../.claude/skills/xt-net/SKILL.md), [operational considerations](../../.claude/skills/xt-net/references/safety.md), and [complete projects](../README.md).
