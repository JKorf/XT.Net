using System;
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
        /// Get client IP
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesclientInfo" /><br />
        /// Endpoint:<br />
        /// GET /future/public/client
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets as quote asset in symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesclientInfo" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/symbol/coins
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string[]>> GetSymbolAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetSymbols" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/symbol/detail
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get futures symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetSymbols" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v3/public/symbol/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesSymbols>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetLeverageBracket" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/leverage/bracket/detail
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTLeverageBrackets>> GetLeverageBracketsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetLeverageBrackets" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/leverage/bracket/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTLeverageBrackets[]>> GetLeverageBracketsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get ticker info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetTicker" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get tickers for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetTicker" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/tickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetDeal" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/deal
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetDepth" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="limit">Number of levels, max 50</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetIndexPrices" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/symbol-index-price
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetIndexPrices" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/index-price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice[]>> GetIndexPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetMarkPrice" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/symbol-mark-price
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgitMarkPrices" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/mark-price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPrice[]>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetKLine" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/kline
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetAggTicker" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/agg-ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTMarketInfo>> GetMarketInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetAggTickers" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/agg-tickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTMarketInfo[]>> GetMarketInfosAsync(CancellationToken ct = default);

        /// <summary>
        /// Funding rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetFundingRate" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/funding-rate
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetTickerBook" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker/book
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetTickerBooks" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker/books
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetFundingRateRecord" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/funding-rate-record
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, PageDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk balance info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetRiskBalance" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/contract/risk-balance
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name</param>
        /// <param name="direction">Page direction</param>
        /// <param name="fromId">From id</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTRiskBalance>>> GetRiskBalanceAsync(string symbol, PageDirection? direction = null, string? fromId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quotesgetOpenInterest" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/contract/open-interest
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_quote_collectiongetFutureInfo" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/cg/contracts
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesSymbolInfo[]>> GetSymbolInfoAsync(CancellationToken ct = default);

    }
}
