using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using XT.Net.Interfaces.Clients;
using XT.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Clients.FuturesApi;
using XT.Net.Clients.SpotApi;
using XT.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Options;

namespace XT.Net.Clients
{
    /// <inheritdoc cref="IXTRestClient" />
    public class XTRestClient : BaseRestClient, IXTRestClient
    {
        #region Api clients
                
         /// <inheritdoc />
        public IXTRestClientFuturesApi UsdtFuturesApi { get; }
        /// <inheritdoc />
        public IXTRestClientFuturesApi CoinFuturesApi { get; }

        /// <inheritdoc />
        public IXTRestClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the XTRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public XTRestClient(Action<XTRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the XTRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public XTRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<XTRestOptions> options) : base(loggerFactory, "XT")
        {
            Initialize(options.Value);
                        
            UsdtFuturesApi = AddApiClient(new XTRestClientUsdtFuturesApi(_logger, this, httpClient, options.Value));
            CoinFuturesApi = AddApiClient(new XTRestClientCoinFuturesApi(_logger, this, httpClient, options.Value));
            SpotApi = AddApiClient(new XTRestClientSpotApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            UsdtFuturesApi.SetOptions(options);
            CoinFuturesApi.SetOptions(options);
            SpotApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<XTRestOptions> optionsDelegate)
        {
            XTRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            UsdtFuturesApi.SetApiCredentials(credentials);
            CoinFuturesApi.SetApiCredentials(credentials);
            SpotApi.SetApiCredentials(credentials);
        }
    }
}
