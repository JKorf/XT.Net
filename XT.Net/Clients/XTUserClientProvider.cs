using XT.Net.Interfaces.Clients;
using XT.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using CryptoExchange.Net.Clients;

namespace XT.Net.Clients
{
    /// <inheritdoc />
    public class XTUserClientProvider : UserClientProvider<
        IXTRestClient,
        IXTSocketClient,
        XTRestOptions,
        XTSocketOptions,
        XTCredentials,
        XTEnvironment
        >, IXTUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => XTExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public XTUserClientProvider(Action<XTOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        public XTUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<XTRestOptions> restOptions,
            IOptions<XTSocketOptions> socketOptions)
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IXTRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<XTRestOptions> options)
            => new XTRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IXTSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<XTSocketOptions> options)
            => new XTSocketClient(options, loggerFactory);
    }
}
