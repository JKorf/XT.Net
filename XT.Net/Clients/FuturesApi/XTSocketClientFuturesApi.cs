using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;
using XT.Net.Objects.Options;
using XT.Net.Objects.Sockets.Subscriptions;

namespace XT.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the XT Futures websocket Api
    /// </summary>
    internal partial class XTSocketClientFuturesApi : SocketApiClient, IXTSocketClientFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal XTSocketClientFuturesApi(ILogger logger, XTSocketOptions options) :
            base(logger, options.Environment.SocketClientAddress!, options, options.FuturesOptions)
        {
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor() => new SystemTextJsonByteMessageAccessor();
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTAuthenticationProvider(credentials);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToXXXUpdatesAsync(Action<DataEvent<XTModel>> onMessage, CancellationToken ct = default)
        {
            var subscription = new XTSubscription<XTModel>(_logger, new [] { "XXX" }, onMessage, false);
            return await SubscribeAsync(subscription, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            return message.GetValue<string>(_idPath);
        }

        /// <inheritdoc />
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public IXTSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
