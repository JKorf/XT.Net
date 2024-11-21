using CryptoExchange.Net.Interfaces;
using XT.Net.Clients;
using XT.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the XT REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IXTRestClient XT(this ICryptoRestClient baseClient) => baseClient.TryGet<IXTRestClient>(() => new XTRestClient());

        /// <summary>
        /// Get the XT Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IXTSocketClient XT(this ICryptoSocketClient baseClient) => baseClient.TryGet<IXTSocketClient>(() => new XTSocketClient());
    }
}
