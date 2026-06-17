using CryptoExchange.Net.Objects;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using XT.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.RateLimiting.Guards;
using XT.Net.Objects.Models;
using XT.Net.Enums;
using XT.Net.Objects.Internal;

namespace XT.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class XTRestClientSpotApiAccount : IXTRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly XTRestClientSpotApi _baseClient;

        internal XTRestClientSpotApiAccount(XTRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Balance

        /// <inheritdoc />
        public async Task<HttpResult<XTBalance>> GetBalanceAsync(string asset, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/balance", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTBalance>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<XTBalances>> GetBalancesAsync(string? assets = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currencies", assets);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/balances", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTBalances>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Address

        /// <inheritdoc />
        public async Task<HttpResult<XTDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/deposit/address", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTDepositAddress>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit History

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTDeposit>>> GetDepositHistoryAsync(string asset, string network, DepositStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            parameters.Add("status", status);
            parameters.Add("fromId", fromId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/deposit/history", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTPage<XTDeposit>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Withdraw

        /// <inheritdoc />
        public async Task<HttpResult<XTId>> WithdrawAsync(string asset, string network, string address, decimal quantity, string? memo = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            parameters.Add("amount", quantity);
            parameters.Add("address", address);
            parameters.Add("memo", memo);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v4/withdraw", XTExchange.RateLimiter.XT, 1, true, limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTId>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdrawal History

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTWithdrawal>>> GetWithdrawalHistoryAsync(string asset, string network, WithdrawalStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("chain", network);
            parameters.Add("status", status);
            parameters.Add("fromId", fromId);
            parameters.Add("direction", direction);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/v4/withdraw/history", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTPage<XTWithdrawal>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<long>> TransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("amount", quantity);
            parameters.Add("bizId", clientId);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v4/balance/transfer", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Sub Account Transfer

        /// <inheritdoc />
        public async Task<HttpResult<long>> SubAccountTransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, long toAccountId, long? fromAccountId = null, string? symbol = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("currency", asset);
            parameters.Add("from", from);
            parameters.Add("to", to);
            parameters.Add("amount", quantity);
            parameters.Add("bizId", clientId);
            parameters.Add("symbol", symbol?.ToLowerInvariant());
            parameters.Add("toAccountId", toAccountId);
            parameters.Add("fromAccountId", fromAccountId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v4/balance/account/transfer", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<long?>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<long>(result);

            return HttpResult.Ok(result, result.Data ?? 0);
        }

        #endregion

        #region Get Websocket Token

        /// <inheritdoc />
        public async Task<HttpResult<string>> GetWebsocketTokenAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "/v4/ws-token", XTExchange.RateLimiter.XT, 1, true);
            var result = await _baseClient.SendAsync<XTToken>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<string>(result);

            return HttpResult.Ok(result, result.Data.AccessToken);
        }

        #endregion
    }
}
