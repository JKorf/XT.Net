using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using XT.Net.Objects.Models;
using System;
using XT.Net.Enums;

namespace XT.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// XT Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IXTRestClientFuturesApiAccount
    {

        /// <summary>
        /// Get balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetContractAccountInfo" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/compat/balance/list
        /// </para>
        /// </summary>
        /// <param name="accountId">["<c>queryAccountId</c>"] Account id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesBalance[]>> GetBalancesAsync(string? accountId = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetContractInfo" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/account/info
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get user asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetBalance" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/balance/detail
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>coin</c>"] Asset name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<XTUserAsset>> GetUserAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get user assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetBalances" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/balance/list
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTUserAsset[]>> GetUserAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account bills
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetBalanceBill" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/balance/bills
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol</param>
        /// <param name="id">Filter by id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTAccountBill>>> GetAccountBillsAsync(string? symbol = null,
            long? id = null,
            PageDirection? direction = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding fee info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetFunding" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/balance/funding-rate-list
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETH-USDT`</param>
        /// <param name="id">["<c>id</c>"] Filter by id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFundingFee>>> GetFundingFeeHistoryAsync(string? symbol = null, string? id = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trading fees
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetStepRate" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/user/step-rate
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFeeRate>> GetFeeRateAsync(CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_useradjustLeverage" /><br />
        /// Endpoint:<br />
        /// POST /future/user/v1/position/adjust-leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetLeverageAsync(string symbol, PositionSide positionSide, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Adjust margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_useradjustMargin" /><br />
        /// Endpoint:<br />
        /// POST /future/user/v1/position/margin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="marginChange">["<c>margin</c>"] The change in margin</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="type">["<c>type</c>"] Adjust type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> AdjustMarginAsync(string symbol, decimal marginChange, PositionSide positionSide, AdjustSide type, CancellationToken ct = default);

        /// <summary>
        /// Get auto deleverage info
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetAdl" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/position/adl
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTAdlInfo[]>> GetAdlInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Set position type
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_userchangePositionType" /><br />
        /// Endpoint:<br />
        /// POST /future/user/v1/position/change-type
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETH-USDT`</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="positionType">["<c>positionType</c>"] Position margin type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionTypeAsync(string symbol, PositionSide positionSide, PositionType positionType, CancellationToken ct = default);

        /// <summary>
        /// Get listen key for starting the user websocket connection
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#futures_usergetListenKey" /><br />
        /// Endpoint:<br />
        /// GET /future/user/v1/user/listen-key
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string>> GetListenKeyAsync(CancellationToken ct = default);
    }
}
