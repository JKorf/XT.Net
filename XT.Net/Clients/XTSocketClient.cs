using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using XT.Net.Clients.FuturesApi;
using XT.Net.Clients.SpotApi;
using XT.Net.Interfaces.Clients;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Options;

namespace XT.Net.Clients
{
    /// <inheritdoc cref="IXTSocketClient" />
    public class XTSocketClient : BaseSocketClient, IXTSocketClient
    {
        #region fields
        #endregion

        #region Api clients
                
         /// <inheritdoc />
        public IXTSocketClientFuturesApi FuturesApi { get; }

         /// <inheritdoc />
        public IXTSocketClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of XTSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public XTSocketClient(Action<XTSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of XTSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public XTSocketClient(IOptions<XTSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "XT")
        {
            Initialize(options.Value);
                        
            FuturesApi = AddApiClient(new XTSocketClientFuturesApi(_logger, options.Value));
            SpotApi = AddApiClient(new XTSocketClientSpotApi(_logger, options.Value));
        }
        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            FuturesApi.SetOptions(options);
            SpotApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<XTSocketOptions> optionsDelegate)
        {
            XTSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            
            FuturesApi.SetApiCredentials(credentials);

            SpotApi.SetApiCredentials(credentials);

        }
    }
}
