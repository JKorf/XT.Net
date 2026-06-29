# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package XT.Net` and it builds; substitute API keys for trading examples.
- **Self-contained** - single file, no shared helpers and no external project setup.
- **Heavily commented** - explains why the pattern matters, not just what to call.
- **Idiomatic** - follows the current XT.Net 3.x REST, WebSocket, and SharedApis shape.

The repository unit test `XT.Net.UnitTests.Documentation.AiExampleCompileTests` compiles every `.cs` file in this directory.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Spot market data, balances, order placement, order status, cleanup |
| `02-futures.cs` | USDT-M futures leverage, market order, positions, close-all pattern |
| `03-websocket.cs` | Spot and futures public streams plus private token/listen-key patterns |
| `04-multi-exchange.cs` | CryptoExchange.Net SharedApis for exchange-agnostic code |
| `05-error-handling.cs` | `HttpResult<T>` handling, retry decisions, symbol validation |

## Running

```bash
dotnet new console -n MyXTApp
cd MyXTApp
dotnet add package XT.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders with your own
dotnet run
```

Use the examples as implementation patterns, not as trading advice. Substitute credentials, symbols, quantities, and risk controls for your application.
