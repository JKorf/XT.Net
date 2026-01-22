using CryptoExchange.Net;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using XT.Net.Objects.Internal;

namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <inheritdoc />
    internal class XTSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private readonly string? _token;
        private readonly Action<DateTime, string?, XTSocketUpdate<T>> _handler;
        private readonly string _topic;
        private readonly string[] _topics;
        private readonly string[]? _symbols;

        /// <summary>
        /// ctor
        /// </summary>
        public XTSubscription(ILogger logger, SocketApiClient client, string topic, string[]? symbols, Action<DateTime, string?, XTSocketUpdate<T>> handler, bool auth, string? token = null, string[]? listenerIdentifiers = null) : base(logger, auth)
        {
            _client = client;
            _handler = handler;
            _token = token;
            _topic = topic;
            _topics = symbols == null ? [topic] : symbols!.Select(x => $"{topic}@{x}").ToArray();
            _symbols = symbols;

            IndividualSubscriptionCount = symbols?.Length ?? 1;
      
            MessageRouter = MessageRouter.CreateWithOptionalTopicFilters<XTSocketUpdate<T>>(
                                topic,
                                listenerIdentifiers == null ? symbols?.ToArray() : listenerIdentifiers.Select(x => x.Split('@')[1]).ToArray(), // When Matcher is removed this can be simplified
                                DoHandleMessage);
        }

        /// <inheritdoc />
        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new XTQuery(_client, new XTSocketRequest
            {
                Id = ExchangeHelpers.NextId().ToString(),
                Method = "subscribe",
                Parameters = _topics,
                ListenKey = _token
            }, null);
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
            }, null);
        }

        /// <inheritdoc />
        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, XTSocketUpdate<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
