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
    internal class XTSubscription<T> : Subscription<XTSocketResponse, XTSocketResponse>
    {
        /// <inheritdoc />
        public override HashSet<string> ListenerIdentifiers { get; set; }

        private readonly string? _token;
        private readonly string[]? _queryIdentifiers;
        private readonly Action<DataEvent<T>> _handler;
        private readonly string[] _topics;

        /// <inheritdoc />
        public override Type? GetMessageType(IMessageAccessor message)
        {
            return typeof(XTSocketUpdate<T>);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public XTSubscription(ILogger logger, string[] topics, Action<DataEvent<T>> handler, bool auth, string? token = null, string[]? listenerIdentifiers = null, string[]? queryIdentifiers = null) : base(logger, auth)
        {
            _handler = handler;
            _token = token;
            _topics = topics;
            _queryIdentifiers = queryIdentifiers;
            ListenerIdentifiers = new HashSet<string>(listenerIdentifiers ?? topics);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "subscribe",
                Parameters = _topics,
                ListenKey = _token,
            }, _queryIdentifiers ?? []);
        }

        /// <inheritdoc />
        public override Query? GetUnsubQuery()
        {
            return new XTQuery(new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "unsubscribe",
                Parameters = _topics,
                ListenKey = _token,
            }, _queryIdentifiers ?? []);
        }

        /// <inheritdoc />
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = (XTSocketUpdate<T>)message.Data;
            _handler.Invoke(message.As(data.Data, data.Event, null, SocketUpdateType.Update));
            return new CallResult(null);
        }
    }
}
