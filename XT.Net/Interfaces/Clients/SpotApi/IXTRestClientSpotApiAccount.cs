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
        /// <para><a href="https://doc.xt.com/#balancebalanceGet" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTBalance>> GetBalanceAsync(string asset, CancellationToken ct = default);

        /// <summary>
        /// Get balances
        /// <para><a href="https://doc.xt.com/#balancecurrenciesGet" /></para>
        /// </summary>
        /// <param name="assets">Filter by assets</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTBalances>> GetBalancesAsync(string? assets = null, CancellationToken ct = default);

        /// <summary>
        /// Get deposit address for an asset
        /// <para><a href="https://doc.xt.com/#deposit_withdrawaldepositAddressGet" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="network">Network</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTDepositAddress>> GetDepositAddressAsync(string asset, string network, CancellationToken ct = default);

        /// <summary>
        /// Get deposit history
        /// <para><a href="https://doc.xt.com/#deposit_withdrawalhistoryDepositGet" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="network">Network</param>
        /// <param name="status">Filter by status</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTDeposit>>> GetDepositHistoryAsync(string asset, string network, DepositStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Withdraw an asset
        /// <para><a href="https://doc.xt.com/#deposit_withdrawalwithdraw" /></para>
        /// </summary>
        /// <param name="asset">The asset, for example `eth`</param>
        /// <param name="network">The network to withdraw on</param>
        /// <param name="quantity">Quantity to withdraw</param>
        /// <param name="address">Address to withdraw to</param>
        /// <param name="memo">Address memo</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTId>> WithdrawAsync(string asset, string network, string address, decimal quantity, string? memo = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal history
        /// <para><a href="https://doc.xt.com/#deposit_withdrawalwithdrawHistory" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="network">Network</param>
        /// <param name="status">Filter by status</param>
        /// <param name="fromId">From id</param>
        /// <param name="direction">Page direction</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<XTPage<XTWithdrawal>>> GetWithdrawalHistoryAsync(string asset, string network, WithdrawalStatus? status = null, long? fromId = null, PageDirection? direction = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets
        /// <para><a href="https://doc.xt.com/#transfertransferPost" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="from">From account</param>
        /// <param name="to">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="clientId">Unique id</param>
        /// <param name="symbol">Isolated margin symbol, required when one of the accounts is Leverage</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> TransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer assets for sub accounts
        /// <para><a href="https://doc.xt.com/#transfersubTransferPost" /></para>
        /// </summary>
        /// <param name="asset">Asset name</param>
        /// <param name="from">From account</param>
        /// <param name="to">To account</param>
        /// <param name="quantity">Quantity to transfer</param>
        /// <param name="clientId">Unique id</param>
        /// <param name="symbol">Isolated margin symbol, required when one of the accounts is Leverage</param>
        /// <param name="toAccountId">To sub account id</param>
        /// <param name="fromAccountId">From sub account id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<long>> SubAccountTransferAsync(string asset, BusinessType from, BusinessType to, decimal quantity, string clientId, long toAccountId, long? fromAccountId = null, string? symbol = null, CancellationToken ct = default);

        /// <summary>
        /// Get a websocket token, required for listening to private websocket streams
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<string>> GetWebsocketTokenAsync(CancellationToken ct = default);
    }
}
