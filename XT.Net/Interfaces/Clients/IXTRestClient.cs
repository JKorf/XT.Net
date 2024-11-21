using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;

namespace XT.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the XT Rest API. 
    /// </summary>
    public interface IXTRestClient : IRestClient
    {   
        /// <summary>
        /// Futures API endpoints
        /// </summary>
        public IXTRestClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Spot API endpoints
        /// </summary>
        public IXTRestClientSpotApi SpotApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
