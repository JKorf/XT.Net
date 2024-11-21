using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Internal;
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/time", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Get Client Ip

        /// <inheritdoc />
        public async Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/public/client", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTClientIp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
