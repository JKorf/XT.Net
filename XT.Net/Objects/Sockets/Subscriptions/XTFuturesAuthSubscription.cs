using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using XT.Net.Objects.Internal;

namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class XTFuturesAuthSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly Action<DateTime, string?, XTSocketUpdate<T>> _handler;
        private readonly string _listenKey;
        private readonly string _topic;

        /// <summary>
        /// ctor
        /// </summary>
        public XTFuturesAuthSubscription(ILogger logger, SocketApiClient client, string topic, string listenKey, Action<DateTime, string?, XTSocketUpdate<T>> handler) : base(logger, false)
        {
            _client = client;
            _handler = handler;
            _listenKey = listenKey;
            _topic = topic;

            MessageRouter = MessageRouter.CreateWithoutTopicFilter<XTSocketUpdate<T>>(topic, DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "subscribe",
                Parameters = [_topic + "@" + _listenKey],
            }, _topic);
        }

        /// <inheritdoc />
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "unsubscribe",
                Parameters = [_topic + "@" + _listenKey],
            }, _topic);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, XTSocketUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
