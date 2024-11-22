using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using XT.Net.Objects.Models;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IXTRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// 
        /// <para><a href="XXX" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get client IP
        /// <para><a href="https://doc.xt.com/#futures_quotesclientInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets as quote asset in symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesclientInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<IEnumerable<string>>> GetSymbolAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a symbol
        /// <para><a href="https://doc.xt.com/#futures_quotesgetSymbols" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get futures symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetSymbols" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesSymbols>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for a symbol
        /// <para><a href="https://doc.xt.com/#futures_quotesgetLeverageBracket" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTLeverageBrackets>> GetLeverageBracketsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for all symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetLeverageBrackets" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTLeverageBrackets>>> GetLeverageBracketsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get ticker info for a symbol
        /// <para><a href="https://doc.xt.com/#futures_quotesgetTicker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get tickers for all symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetTicker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesTicker>>> GetTickerAsync(CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para><a href="https://doc.xt.com/#futures_quotesgetDeal" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesTrade>>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para><a href="https://doc.xt.com/#futures_quotesgetDepth" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Number of levels, max 50</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetIndexPrices" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetIndexPrices" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTPrice>>> GetIndexPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgetMarkPrice" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para><a href="https://doc.xt.com/#futures_quotesgitMarkPrices" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTPrice>>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://doc.xt.com/#futures_quotesgetKLine" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesKline>>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para><a href="https://doc.xt.com/#futures_quotesgetAggTicker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTMarketInfo>> GetMarketInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para><a href="https://doc.xt.com/#futures_quotesgetAggTickers" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTMarketInfo>>> GetMarketInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Funding rate
        /// <para><a href="https://doc.xt.com/#futures_quotesgetFundingRate" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para><a href="https://doc.xt.com/#futures_quotesgetTickerBook" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para><a href="https://doc.xt.com/#futures_quotesgetTickerBooks" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesBookTicker>>> GetBookTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://doc.xt.com/#futures_quotesgetFundingRateRecord" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, PageDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk balance info
        /// <para><a href="https://doc.xt.com/#futures_quotesgetRiskBalance" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="direction">Page direction</param>
        /// <param name="fromId">From id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTRiskBalance>>> GetRiskBalanceAsync(string symbol, PageDirection? direction = null, string? fromId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest info
        /// <para><a href="https://doc.xt.com/#futures_quotesgetOpenInterest" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// <para><a href="https://doc.xt.com/#futures_quote_collectiongetFutureInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesSymbolInfo>>> GetSymbolInfoAsync(CancellationToken ct = default);

    }
}
