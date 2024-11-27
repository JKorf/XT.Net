using System.Collections.Generic;
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
        /// <para><a href="https://doc.xt.com/#futures_usergetContractAccountInfo" /></para>
        /// </summary>
        /// <param name="accountId">Account id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTFuturesBalance>>> GetBalancesAsync(string? accountId = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info
        /// <para><a href="https://doc.xt.com/#futures_usergetContractInfo" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFuturesAccountInfo>> GetAccountInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get user asset
        /// <para><a href="https://doc.xt.com/#futures_usergetBalance" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<XTUserAsset>> GetUserAssetAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get user assets
        /// <para><a href="https://doc.xt.com/#futures_usergetBalances" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTUserAsset>>> GetUserAssetsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get account bills
        /// <para><a href="https://doc.xt.com/#futures_usergetBalanceBill" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="id">Filter by id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTAccountBill>>> GetAccountBillsAsync(string? symbol = null,
            long? id = null,
            PageDirection? direction = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding fee info
        /// <para><a href="https://doc.xt.com/#futures_usergetFunding" /></para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETH-USDT`</param>
        /// <param name="id">Filter by id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTFundingFee>>> GetFundingFeeHistoryAsync(string? symbol = null, string? id = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trading fees
        /// <para><a href="https://doc.xt.com/#futures_usergetStepRate" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTFeeRate>> GetFeeRateAsync(CancellationToken ct = default);

        /// <summary>
        /// Set leverage
        /// <para><a href="https://doc.xt.com/#futures_useradjustLeverage" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetLeverageAsync(string symbol, PositionSide positionSide, int leverage, CancellationToken ct = default);

        /// <summary>
        /// Adjust margin
        /// <para><a href="https://doc.xt.com/#futures_useradjustMargin" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="marginChange">The change in margin</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="type">Adjust type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> AdjustMarginAsync(string symbol, decimal marginChange, PositionSide positionSide, AdjustSide type, CancellationToken ct = default);

        /// <summary>
        /// Get auto deleverage info
        /// <para><a href="https://doc.xt.com/#futures_usergetAdl" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<IEnumerable<XTAdlInfo>>> GetAdlInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Set position type
        /// <para><a href="https://doc.xt.com/#futures_userchangePositionType" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETH-USDT`</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="positionType">Position margin type</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionTypeAsync(string symbol, PositionSide positionSide, PositionType positionType, CancellationToken ct = default);

        /// <summary>
        /// Get listen key for starting the user websocket connection
        /// <para><a href="https://doc.xt.com/#futures_usergetListenKey" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<string>> GetListenKeyAsync(CancellationToken ct = default);
    }
}
