using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
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
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<WebCallResult<XTClientIp>> GetClientIpAsync(CancellationToken ct = default);
    }
}
