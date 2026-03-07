using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using XT.Net.Objects.Models;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IXTRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market1serverInfo" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get your client IP
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market1clientInfo" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/client
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol information
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market2symbol" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/symbol
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="version">["<c>version</c>"] Version number from a previous request. When the result has not been changed since this version number there will be no response</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTSymbols>> GetSymbolsAsync(string? symbol = null, IEnumerable<string>? symbols = null, string? version = null, CancellationToken ct = default);

        /// <summary>
        /// Get the order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market3depth" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows of the book to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market4kline" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/kline
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="interval">["<c>interval</c>"] Interval of the klines</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market5tradeRecent" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/trade/recent
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market6tradeHistory" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/trade/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="fromId">["<c>fromId</c>"] From id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, PageDirection? direction = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market7allTicker" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTTicker[]>> GetTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get price tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market8tickerPrice" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPriceTicker[]>> GetPriceTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get best orderbook offers
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market9tickerBook" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/ticker/book
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTBookTicker[]>> GetBookTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price stats
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#market10ticker24h" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/ticker/24h
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">["<c>symbols</c>"] Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XT24HTicker[]>> Get24HTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#balancecurrenciesGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/currencies
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTAssets>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get assets network info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#deposit_withdrawalsupportedCurrenciesGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/wallet/support/currency
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTAssetNetworks[]>> GetAssetNetworksAsync(CancellationToken ct = default);

    }
}
