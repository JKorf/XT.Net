using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using Microsoft.Extensions.Logging;
using System;
using XT.Net.Objects.Internal;

namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc cref="Subscription" />
    internal class XTFuturesAuthSubscription<T> : Subscription, IXTAuthenticatedSubscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, XTSocketUpdate<T>> _handler;
        private readonly string _topic;

        /// <inheritdoc />
        public string? Token { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public XTFuturesAuthSubscription(ILogger logger, SocketApiClient client, string topic, string listenKey, Action<DateTime, string?, XTSocketUpdate<T>> handler) : base(logger, false)
        {
            _client = client;
            _handler = handler;
            Token = listenKey;
            _topic = topic;

            MessageRouter = MessageRouter.CreateForEvent<XTSocketUpdate<T>>(topic, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "subscribe",
                Parameters = [_topic + "@" + Token],
            }, _topic);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "unsubscribe",
                Parameters = [_topic + "@" + Token],
            }, _topic);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, XTSocketUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.Ok();
        }
    }
}
