using CryptoExchange.Net.Objects;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;
using CryptoExchange.Net.RateLimiting.Guards;
using XT.Net.Enums;

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
        public async Task<HttpResult<XTFuturesBalance[]>> GetBalancesAsync(string? accountId = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("queryAccountId", accountId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/compat/balance/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesBalance[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/account/info", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync<XTFuturesAccountInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Asset

        /// <inheritdoc />
        public async Task<HttpResult<XTUserAsset>> GetUserAssetAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("coin", asset);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/balance/detail", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTUserAsset>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get User Assets

        /// <inheritdoc />
        public async Task<HttpResult<XTUserAsset[]>> GetUserAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/balance/list", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTUserAsset[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Account Bills

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTAccountBill>>> GetAccountBillsAsync(
            string? symbol = null,
            long? id = null,
            PageDirection? direction = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/balance/bills", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTAccountBill>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTFundingFee>>> GetFundingFeeHistoryAsync(string? symbol = null, string? id = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("id", id);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/balance/funding-rate-list", XTExchange.RateLimiter.RestFutures, 1, true);
            var result = await _baseClient.SendAsync<XTPage<XTFundingFee>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Fee Rate

        /// <inheritdoc />
        public async Task<HttpResult<XTFeeRate>> GetFeeRateAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/user/step-rate", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFeeRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult> SetLeverageAsync(string symbol, PositionSide positionSide, int leverage, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("positionSide", positionSide);
            parameters.Add("leverage", leverage);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/user/v1/position/adjust-leverage", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Adjust Margin

        /// <inheritdoc />
        public async Task<HttpResult> AdjustMarginAsync(string symbol, decimal marginChange, PositionSide positionSide, AdjustSide type, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("margin", marginChange);
            parameters.Add("positionSide", positionSide);
            parameters.Add("type", type);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/user/v1/position/margin", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Adl Info

        /// <inheritdoc />
        public async Task<HttpResult<XTAdlInfo[]>> GetAdlInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/position/adl", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTAdlInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Set Position Type

        /// <inheritdoc />
        public async Task<HttpResult> SetPositionTypeAsync(string symbol, PositionSide positionSide, PositionType positionType, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("positionSide", positionSide);
            parameters.Add("positionType", positionType);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/future/user/v1/position/change-type", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Listen Key

        /// <inheritdoc />
        public async Task<HttpResult<string>> GetListenKeyAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/user/v1/user/listen-key", XTExchange.RateLimiter.RestFutures, 1, true, limitGuard: new SingleLimitGuard(200, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<string>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}
