using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using XT.Net.Objects.Models;
using System;
using XT.Net.Enums;

namespace XT.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// XT Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IXTRestClientSpotApiAccount
    {
        /// <summary>
        /// Get balance for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#balancebalanceGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/balance
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTBalance>> GetBalanceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#balancecurrenciesGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/balances
        /// </para>
        /// </summary>
        /// <param name="assets">["<c>currencies</c>"] Filter by assets</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTBalances>> GetBalancesAsync(string? assets = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#deposit_withdrawaldepositAddressGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/deposit/address
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="network">["<c>chain</c>"] Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#deposit_withdrawalhistoryDepositGet" /><br />
        /// Endpoint:<br />
        /// GET /v4/deposit/history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="network">["<c>chain</c>"] Network</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="fromId">["<c>fromId</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTDeposit>>> GetDepositHistoryAsync(string asset, string network, DepositStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#deposit_withdrawalwithdraw" /><br />
        /// Endpoint:<br />
        /// POST /v4/withdraw
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] The asset, for example `eth`</param>
        /// <param name="network">["<c>chain</c>"] The network to withdraw on</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to withdraw</param>
        /// <param name="address">["<c>address</c>"] Address to withdraw to</param>
        /// <param name="memo">["<c>memo</c>"] Address memo</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTId>> WithdrawAsync(string asset, string network, string address, decimal quantity, string? memo = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#deposit_withdrawalwithdrawHistory" /><br />
        /// Endpoint:<br />
        /// GET /v4/withdraw/history
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="network">["<c>chain</c>"] Network</param>
        /// <param name="status">["<c>status</c>"] Filter by status</param>
        /// <param name="fromId">["<c>fromId</c>"] From id</param>
        /// <param name="direction">["<c>direction</c>"] Page direction</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTWithdrawal>>> GetWithdrawalHistoryAsync(string asset, string network, WithdrawalStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#transfertransferPost" /><br />
        /// Endpoint:<br />
        /// POST /v4/balance/transfer
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="from">["<c>from</c>"] From account</param>
        /// <param name="to">["<c>to</c>"] To account</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="clientId">["<c>bizId</c>"] Unique id</param>
        /// <param name="symbol">["<c>symbol</c>"] Isolated margin symbol, required when one of the accounts is Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> TransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets for sub accounts
        /// <para>
        /// Docs:<br />
        /// <a href="https://doc.xt.com/#transfersubTransferPost" /><br />
        /// Endpoint:<br />
        /// POST /v4/balance/account/transfer
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>currency</c>"] Asset name</param>
        /// <param name="from">["<c>from</c>"] From account</param>
        /// <param name="to">["<c>to</c>"] To account</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity to transfer</param>
        /// <param name="clientId">["<c>bizId</c>"] Unique id</param>
        /// <param name="toAccountId">["<c>toAccountId</c>"] To sub account id</param>
        /// <param name="fromAccountId">["<c>fromAccountId</c>"] From sub account id</param>
        /// <param name="symbol">["<c>symbol</c>"] Isolated margin symbol, required when one of the accounts is Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> SubAccountTransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, long toAccountId, long? fromAccountId = null, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get a websocket token, required for listening to private websocket streams
        /// <para>
        /// Endpoint:<br />
        /// POST /v4/ws-token
        /// </para>
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<string>> GetWebsocketTokenAsync(CancellationToken ct = default);
    }
}
