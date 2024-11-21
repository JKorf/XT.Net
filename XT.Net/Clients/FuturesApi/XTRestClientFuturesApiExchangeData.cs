using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class XTRestClientFuturesApiExchangeData : IXTRestClientFuturesApiExchangeData
    {
        private readonly XTRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal XTRestClientFuturesApiExchangeData(ILogger logger, XTRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "XXX", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTModel>(request, null, ct).ConfigureAwait(false);
            throw new NotImplementedException();
        }

        #endregion
    }
}
