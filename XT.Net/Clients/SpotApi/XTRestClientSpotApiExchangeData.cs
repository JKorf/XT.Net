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
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class XTRestClientSpotApiExchangeData : IXTRestClientSpotApiExchangeData
    {
        private readonly XTRestClientSpotApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal XTRestClientSpotApiExchangeData(ILogger logger, XTRestClientSpotApi baseClient)
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
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/client", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTClientIp>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Symbols

        /// <inheritdoc />
        public async Task<WebCallResult<XTSymbols>> GetSymbolsAsync(string? symbol = null, IEnumerable<string>? symbols = null, string? version = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            parameters.AddOptional("version", version);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/symbol", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTSymbols>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<XTOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/depth", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTOrderBook>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<XTKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalMillisecondsString("startTime", startTime);
            parameters.AddOptionalMillisecondsString("endTime", endTime);
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/kline", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTKline[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Recent Trades

        /// <inheritdoc />
        public async Task<WebCallResult<XTTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddOptional("limit", limit);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/trade/recent", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTrade[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Trade History

        /// <inheritdoc />
        public async Task<WebCallResult<XTPage<XTTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, PageDirection? direction = null, long? fromId = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol.ToLowerInvariant());
            parameters.AddOptional("limit", limit);
            parameters.AddEnum("direction", direction ?? PageDirection.Previous);
            parameters.AddOptional("fromId", fromId);
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/trade/history", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPage<XTTrade>>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<XTTicker[]>> GetTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/ticker", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Price Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<XTPriceTicker[]>> GetPriceTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/ticker/price", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTPriceTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Book Tickers

        /// <inheritdoc />
        public async Task<WebCallResult<XTBookTicker[]>> GetBookTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/ticker/book", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XTBookTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get 24H Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<XT24HTicker[]>> Get24HTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol?.ToLowerInvariant());
            parameters.AddOptional("symbols", symbols == null ? null : string.Join(",", symbols));
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/ticker/24h", XTExchange.RateLimiter.XT, 1, false, limitGuard: new SingleLimitGuard(10, TimeSpan.FromSeconds(1), RateLimitWindowType.Sliding));
            var result = await _baseClient.SendAsync<XT24HTicker[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Assets

        /// <inheritdoc />
        public async Task<WebCallResult<XTAssets>> GetAssetsAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/currencies", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTAssets>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Asset Networks

        /// <inheritdoc />
        public async Task<WebCallResult<XTAssetNetworks[]>> GetAssetNetworksAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/v4/public/wallet/support/currency", XTExchange.RateLimiter.XT, 1, false);
            var result = await _baseClient.SendAsync<XTAssetNetworks[]>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
