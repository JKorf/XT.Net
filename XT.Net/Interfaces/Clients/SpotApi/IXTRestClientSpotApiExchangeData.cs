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
        /// <para><a href="https://doc.xt.com/#market1serverInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get your client IP
        /// <para><a href="https://doc.xt.com/#market1clientInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol information
        /// <para><a href="https://doc.xt.com/#market2symbol" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="version">Version number from a previous request. When the result has not been changed since this version number there will be no response</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTSymbols>> GetSymbolsAsync(string? symbol = null, IEnumerable<string>? symbols = null, string? version = null, CancellationToken ct = default);

        /// <summary>
        /// Get the order book
        /// <para><a href="https://doc.xt.com/#market3depth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Number of rows of the book to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get klines
        /// <para><a href="https://doc.xt.com/#market4kline" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Interval of the klines</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTKline>>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades
        /// <para><a href="https://doc.xt.com/#market5tradeRecent" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para><a href="https://doc.xt.com/#market6tradeHistory" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="direction">Page direction</param>
        /// <param name="fromId">From id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTTrade>>> GetTradeHistoryAsync(string symbol, int? limit = null, PageDirection? direction = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get tickers
        /// <para><a href="https://doc.xt.com/#market7allTicker" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTTicker>>> GetTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get price tickers
        /// <para><a href="https://doc.xt.com/#market8tickerPrice" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTPriceTicker>>> GetPriceTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get best orderbook offers
        /// <para><a href="https://doc.xt.com/#market9tickerBook" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTBookTicker>>> GetBookTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get 24h price stats
        /// <para><a href="https://doc.xt.com/#market10ticker24h" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `eth_usdt`</param>
        /// <param name="symbols">Filter by symbols</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XT24HTicker>>> Get24HTickersAsync(string? symbol = null, IEnumerable<string>? symbols = null, CancellationToken ct = default);

        /// <summary>
        /// Get assets
        /// <para><a href="https://doc.xt.com/#balancecurrenciesGet" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTAssets>> GetAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get assets network info
        /// <para><a href="https://doc.xt.com/#deposit_withdrawalsupportedCurrenciesGet" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTAssetNetworks>>> GetAssetNetworksAsync(CancellationToken ct = default);

    }
}
