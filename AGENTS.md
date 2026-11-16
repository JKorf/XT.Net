# Working on XT.Net

Consumer guidance belongs to the [XT.Net skill](.claude/skills/xt-net/SKILL.md); [llms.txt](llms.txt) is its discovery index. This file describes repository development.

## Layout and conventions

- [Public client interfaces](XT.Net/Interfaces/Clients) and [implementations](XT.Net/Clients): spot and futures REST, socket subscriptions, V1 compatibility, and Shared API V2 adapters.
- [Models and options](XT.Net/Objects), [enums](XT.Net/Enums), and [converters](XT.Net/Converters): wire models, configuration, enum mappings, and JSON source-generation metadata.
- [DI extensions](XT.Net/ExtensionMethods), [local order books](XT.Net/SymbolOrderBooks), [tests](XT.Net.UnitTests), and [examples](Examples/README.md).

Follow neighboring endpoints for request construction, signing, validation, result handling, cancellation, nullable annotations, and XML documentation. Use `ConfigureAwait(false)` on library awaits as required by the analyzer. Update public interfaces with implementations, wire models with serialization metadata, and Shared API adapters with accurate capability options. Update relevant request/response fixtures when behavior changes.

Keep spot, USDT-M, and Coin-M REST product semantics distinct; futures sockets share one root. Preserve token/listen-key lifecycle and public API compatibility. Validate every target in the [project](XT.Net/XT.Net.csproj). Keep unrelated dependencies, release settings, and generated output outside focused changes.

## Build and validation

Use the SDK configured in [CI](.github/workflows/dotnet.yml). From the repository root:

```powershell
dotnet restore XT.Net.sln
dotnet build XT.Net.sln --no-restore
dotnet test XT.Net.UnitTests/XT.Net.UnitTests.csproj --no-build --filter "FullyQualifiedName~AiExampleCompileTests"
```

Debug normally builds only `netstandard2.0` and `net11.0`. Validate all declared target frameworks and the package with:

```powershell
dotnet build XT.Net/XT.Net.csproj -c Release -p:BuildAllFrameworks=true
dotnet pack XT.Net/XT.Net.csproj -c Release --no-build
```

`Documentation/AiExampleCompileTests.cs` compiles standalone examples without running their exchange operations. Run relevant fixture tests for implementation changes. Live REST/socket integration fixtures have execution gates; shared REST integration uses `ManualRun` or `INTEGRATION=1`. Do not enable live tests for documentation validation. Build the [complete example solution](Examples/Examples.sln) without running it; some examples change live account state when executed with valid credentials.

## Documentation ownership

Keep routing in `.claude/skills/xt-net/references/api-surfaces.md`, focused snippets in `references/usage.md`, and operational considerations in `references/safety.md`. `SKILL.md` routes to those references, `CLAUDE.md` imports this file, and `llms.txt` is a short link index.

Pack `.claude/skills/**` under `skills/`, without appending `%(RecursiveDir)`. Validate relative links/anchors and compile changed snippets/examples. Inspect the archive for exactly five skill files under `skills/xt-net/`, matching source bytes. Label repository-source/example links inside the skill as checkout-only so the packaged guidance remains usable independently.
