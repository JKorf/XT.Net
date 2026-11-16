# Examples

| Project | Purpose |
| --- | --- |
| [API](XT.Examples.Api/XT.Examples.Api.csproj) | Inject clients into a web API |
| [Console](XT.Examples.Console/XT.Examples.Console.csproj) | Basic client use |
| [Order book](XT.Examples.OrderBook/XT.Examples.OrderBook.csproj) | Maintained local order book |
| [Tracker](XT.Examples.Tracker/XT.Examples.Tracker.csproj) | User-data tracking |
| [Order placement](XT.Examples.OrderPlacement/XT.Examples.OrderPlacement.csproj) | Spot and futures submissions |
| [Standalone programs](ai-friendly/README.md) | Focused REST, socket, V2, and error-handling examples |

Build with `dotnet build Examples/Examples.sln` from the repository root. Compilation does not execute exchange calls. Running examples can contact live markets or change account state when credentials are supplied.

See the [consumer skill](../.claude/skills/xt-net/SKILL.md) and [repository instructions](../AGENTS.md).
