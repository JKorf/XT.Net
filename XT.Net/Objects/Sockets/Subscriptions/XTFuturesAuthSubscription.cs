using CryptoExchange.Net;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class XTFuturesAuthSubscription<T> : Subscription<XTSocketResponse, XTSocketResponse>
    {
        private readonly string[]? _queryIdentifiers;
        private readonly Action<DataEvent<T>> _handler;
        private readonly string _listenKey;
        private readonly string _topic;

        /// <summary>
        /// ctor
        /// </summary>
        public XTFuturesAuthSubscription(ILogger logger, string topic, string listenKey, Action<DataEvent<T>> handler) : base(logger, false)
        {
            _handler = handler;
            _queryIdentifiers = [topic+"@invalid_listen_key"];
            _listenKey = listenKey;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<XTSocketUpdate<T>>(topic, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "subscribe",
                Parameters = [_topic + "@" + _listenKey],
            }, _queryIdentifiers ?? []);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            return new XTQuery(new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "unsubscribe",
                Parameters = [_topic + "@" + _listenKey],
            }, _queryIdentifiers ?? []);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<XTSocketUpdate<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Data, message.Data.Event, null, SocketUpdateType.Update));
            return CallResult.SuccessResult;
        }
    }
}
