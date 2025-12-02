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
        private readonly Action<DateTime, string?, XTSocketUpdate<T>> _handler;
        private readonly string _topic;
        private readonly string[] _topics;
        private readonly string[]? _symbols;

        /// <summary>
        /// ctor
        /// </summary>
        public XTSubscription(ILogger logger, SocketApiClient client, string topic, string[]? symbols, Action<DateTime, string?, XTSocketUpdate<T>> handler, bool auth, string? token = null, string[]? listenerIdentifiers = null, string[]? queryIdentifiers = null) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _token = token;
            _topic = topic;
            _topics = symbols == null ? [topic] : symbols!.Select(x => $"{topic}@{x}").ToArray();
            _symbols = symbols;
            _queryIdentifiers = queryIdentifiers;

            MessageMatcher = MessageMatcher.Create<XTSocketUpdate<T>>(listenerIdentifiers ?? _topics, DoHandleMessage);            
            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<XTSocketUpdate<T>>(topic, symbols?.ToArray(), DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
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
        protected override Query? GetUnsubQuery(SocketConnection connection)
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
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, XTSocketUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
