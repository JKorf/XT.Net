using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using XT.Net.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;
using CryptoExchange.Net.RateLimiting.Guards;

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


        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesBalance>>> GetBalancesAsync(string? accountId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("queryAccountId", accountId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/user/v1/compat/balance/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<IEnumerable<XTFuturesBalance>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
