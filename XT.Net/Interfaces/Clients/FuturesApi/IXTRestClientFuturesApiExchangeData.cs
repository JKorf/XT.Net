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
        /// <a href="https://doc.xt.com/docs/spot/Market/GetServerTime" /><br />
        /// Endpoint:<br />
        /// GET /v4/public/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get client IP
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-trading-pair-currency" /><br />
        /// Endpoint:<br />
        /// GET /future/public/client
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);

        /// <summary>
        /// Get supported assets as quote asset in symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/GetClientIp" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/symbol/coins
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<HttpResult<string[]>> GetSymbolAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get info on a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-configuration-information-for-single-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/symbol/detail
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesSymbol>> GetSymbolAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get futures symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-configuration-information-for-listed-and-tradeable-symbols" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v3/public/symbol/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesSymbols>> GetSymbolsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/see-leverage-stratification-of-single-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/leverage/bracket/detail
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTLeverageBrackets>> GetLeverageBracketsAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/see-leverage-stratification-of-single-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/leverage/bracket/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTLeverageBrackets[]>> GetLeverageBracketsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get ticker info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-market-information-for-all-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get tickers for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-market-information-for-specific-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/tickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-latest-transaction-information-of-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/deal
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="limit">["<c>num</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-depth-data-of-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="limit">["<c>level</c>"] Number of levels, max 50</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-index-price-for-all-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/symbol-index-price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPrice>> GetIndexPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-index-price-for-single-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/index-price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPrice[]>> GetIndexPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get index price for a symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-mark-price-for-single-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/symbol-mark-price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get index prices for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-mark-price-for-all-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/mark-price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPrice[]>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-trading-pair-information-of-kline" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/kline
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesKline[]>> GetKlinesAsync(string symbol, FuturesKlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-aggregated-market-information-for-specific-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/agg-ticker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTMarketInfo>> GetMarketInfoAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get market info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get_aggregated_market_information_for_all_trading_pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/agg-tickers
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTMarketInfo[]>> GetMarketInfosAsync(CancellationToken ct = default);

        /// <summary>
        /// Funding rate
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-funding-rate-information" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/funding-rate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFundingRate>> GetFundingRateAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-ask-bid-market-information-for-specific-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker/book
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-ask-bid-market-information-for-all-trading-pairs" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/ticker/books
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-funding-rate-records" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/q/funding-rate-record
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>id</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPage<XTFundingRateHistory>>> GetFundingRateHistoryAsync(string symbol, long? fromId = null, PageDirection? direction = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get risk balance info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-trading-pair-risk-fund-balance" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/contract/risk-balance
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="fromId">["<c>id</c>"] From id</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTPage<XTRiskBalance>>> GetRiskBalanceAsync(string symbol, PageDirection? direction = null, string? fromId = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get open interest info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/MarketData/get-the-open-position-of-a-trading-pair" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/contract/open-interest
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `eth_usdt`</param>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTOpenInterest>> GetOpenInterestAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get symbol info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/docs/futures/Quote%20collection/get-futures-info" /><br />
        /// Endpoint:<br />
        /// GET /future/market/v1/public/cg/contracts
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<HttpResult<XTFuturesSymbolInfo[]>> GetSymbolInfoAsync(CancellationToken ct = default);

    }
}
