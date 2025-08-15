using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class XTSubscription<T> : Subscription<XTSocketResponse, XTSocketResponse>
    {
        private readonly SocketApiClient _client;
        private readonly string? _token;
        private readonly string[]? _queryIdentifiers;
        private readonly Action<DataEvent<T>> _handler;
        private readonly string[] _topics;

        /// <summary>
        /// ctor
        /// </summary>
        public XTSubscription(ILogger logger, SocketApiClient client, string[] topics, Action<DataEvent<T>> handler, bool auth, string? token = null, string[]? listenerIdentifiers = null, string[]? queryIdentifiers = null) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _token = token;
            _topics = topics;
            _queryIdentifiers = queryIdentifiers;
            MessageMatcher = MessageMatcher.Create<XTSocketUpdate<T>>(listenerIdentifiers ?? topics, DoHandleMessage);
        }

        /// <inheritdoc />
        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
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
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "unsubscribe",
                Parameters = _topics,
                ListenKey = _token,
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
