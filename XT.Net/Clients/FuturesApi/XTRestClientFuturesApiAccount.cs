using CryptoExchange.Net.Objects;
using XT.Net.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.FuturesApi;

namespace XT.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class XTRestClientFuturesApiAccount : IXTRestClientFuturesApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly XTRestClientFuturesApi _baseClient;

        internal XTRestClientFuturesApiAccount(XTRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }
    }
}
