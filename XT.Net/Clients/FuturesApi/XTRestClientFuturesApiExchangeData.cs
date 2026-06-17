using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Guards;
using Microsoft.Extensions.Logging;
using XT.Net.Enums;
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
        public async Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.ClientOptions.Environment.SpotRestClientAddress, "/v4/public/time", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendRawAsync<XTRestResponse<XTServerTime>>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<DateTime>(result);

            return HttpResult.Ok(result, result.Data.Result!.ServerTime);
        }

        #endregion

        #region Get Client Ip

        /// <inheritdoc />
        public async Task<HttpResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/public/client", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTClientIp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Assets

        /// <inheritdoc />
        public async Task<HttpResult<string[]>> GetSymbolAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/symbol/coins", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<string[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/symbol/detail", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesSymbol>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesSymbols>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v3/public/symbol/list", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesSymbols>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<HttpResult<XTLeverageBrackets>> GetLeverageBracketsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/leverage/bracket/detail", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTLeverageBrackets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<HttpResult<XTLeverageBrackets[]>> GetLeverageBracketsAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/leverage/bracket/list", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTLeverageBrackets[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/ticker", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/tickers", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("num", limit ?? 100);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/deal", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("level", limit ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/depth", XTExchange.RateLimiter.RestFutures, 1, false, limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<HttpResult<XTPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/symbol-index-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<HttpResult<XTPrice[]>> GetIndexPricesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/index-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<HttpResult<XTPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/symbol-mark-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<HttpResult<XTPrice[]>> GetMarkPricesAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/mark-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("interval", interval);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/kline", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Market Info

        /// <inheritdoc />
        public async Task<HttpResult<XTMarketInfo>> GetMarketInfoAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/agg-ticker", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTMarketInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Market Info

        /// <inheritdoc />
        public async Task<HttpResult<XTMarketInfo[]>> GetMarketInfosAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/agg-tickers", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTMarketInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<HttpResult<XTFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/funding-rate", XTExchange.RateLimiter.RestFutures, 1, false, limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/ticker/book", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesBookTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/ticker/books", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, PageDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("id", fromId);
            parameters.Add("direction", direction);
            parameters.Add("", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/q/funding-rate-record", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPage<XTFundingRateHistory>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Risk Balance

        /// <inheritdoc />
        public async Task<HttpResult<XTPage<XTRiskBalance>>> GetRiskBalanceAsync(string symbol, PageDirection? direction = null, string? fromId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.Add("direction", direction);
            parameters.Add("id", fromId);
            parameters.Add("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/contract/risk-balance", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPage<XTRiskBalance>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<HttpResult<XTOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol.ToLowerInvariant());
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/contract/open-interest", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Info

        /// <inheritdoc />
        public async Task<HttpResult<XTFuturesSymbolInfo[]>> GetSymbolInfoAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(XTExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "/future/market/v1/public/cg/contracts", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendRawAsync<XTFuturesSymbolInfo[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
