using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CryptoExchange.Net.Objects;
using XT.Net.Objects.Models;

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
    }
}
