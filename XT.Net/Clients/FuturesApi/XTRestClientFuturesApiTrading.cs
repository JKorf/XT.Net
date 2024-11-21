using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using XT.Net.Interfaces.Clients.FuturesApi;

namespace XT.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class XTRestClientFuturesApiTrading : IXTRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly XTRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal XTRestClientFuturesApiTrading(ILogger logger, XTRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }
    }
}
