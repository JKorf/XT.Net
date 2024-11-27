using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
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
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/time", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendToAddressRawAsync<XTRestResponse<XTServerTime>>(_baseClient._baseClient.SpotApi.BaseAddress, request, null, ct).ConfigureAwait(false);
            return result.As(result.Data.Result?.ServerTime ?? default);
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

        #region Get Symbol Assets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<string>>> GetSymbolAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/symbol/coins", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<string>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/symbol/detail", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesSymbol>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesSymbols>> GetSymbolsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v3/public/symbol/list", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesSymbols>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<WebCallResult<XTLeverageBrackets>> GetLeverageBracketsAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/leverage/bracket/detail", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTLeverageBrackets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTLeverageBrackets>>> GetLeverageBracketsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/leverage/bracket/list", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTLeverageBrackets>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/ticker", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesTicker>>> GetTickerAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/tickers", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTFuturesTicker>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("num", limit ?? 100);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/deal", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTFuturesTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("level", limit ?? 20);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/depth", XTExchange.RateLimiter.RestFutures, 1, false, limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFuturesOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<WebCallResult<XTPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/symbol-index-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTPrice>>> GetIndexPricesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/index-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTPrice>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<WebCallResult<XTPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/symbol-mark-price", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPrice>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Index Prices

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTPrice>>> GetMarkPricesAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/mark-pricee", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTPrice>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/kline", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTFuturesKline>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Market Info

        /// <inheritdoc />
        public async Task<WebCallResult<XTMarketInfo>> GetMarketInfoAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/agg-ticker", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTMarketInfo>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Market Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTMarketInfo>>> GetMarketInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/agg-tickers", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTMarketInfo>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate

        /// <inheritdoc />
        public async Task<WebCallResult<XTFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/funding-rate", XTExchange.RateLimiter.RestFutures, 1, false, limitGuard: new SingleLimitGuard(1, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTFundingRate>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<XTFuturesBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/ticker/book", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTFuturesBookTicker>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesBookTicker>>> GetBookTickersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/ticker/books", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<IEnumerable<XTFuturesBookTicker>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, PageDirection? direction = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("id", fromId);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptional("", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/q/funding-rate-record", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPage<XTFundingRateHistory>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Risk Balance

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTRiskBalance>>> GetRiskBalanceAsync(string symbol, PageDirection? direction = null, string? fromId = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalEnum("direction", direction);
            parameters.AddOptional("id", fromId);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/contract/risk-balance", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTPage<XTRiskBalance>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Open Interest

        /// <inheritdoc />
        public async Task<WebCallResult<XTOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/contract/open-interest", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendAsync<XTOpenInterest>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbol Info

        /// <inheritdoc />
        public async Task<WebCallResult<IEnumerable<XTFuturesSymbolInfo>>> GetSymbolInfoAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/future/market/v1/public/cg/contracts", XTExchange.RateLimiter.RestFutures, 1, false);
            var result = await _baseClient.SendRawAsync<IEnumerable<XTFuturesSymbolInfo>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
