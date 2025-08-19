using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Objects.Sockets
{
    internal class XTQuery : Query<XTSocketResponse>
    {
        private readonly SocketApiClient _client;

        public XTQuery(SocketApiClient client, XTSocketRequest request, params string[] additionalIdentifiers) : base(request, false, 1)
        {
            _client = client;
            var checkers = new List<MessageHandlerLink>();
            checkers.Add(new MessageHandlerLink<XTSocketResponse>(MessageLinkType.Full, request.Id, HandleMessage));

            foreach (string identifier in additionalIdentifiers)
                checkers.Add(new MessageHandlerLink<XTSocketResponse>(MessageLinkType.Full, identifier, HandleMessage));

            MessageMatcher = MessageMatcher.Create(checkers.ToArray());
        }

        public override CallResult<object> Deserialize(IMessageAccessor message, Type type)
        {
            if (!message.IsValid)
                return new CallResult<object>(new XTSocketResponse() { Code = -1, Message = "Invalid listen key" });

            return base.Deserialize(message, type);
        }

        public CallResult<XTSocketResponse> HandleMessage(SocketConnection connection, DataEvent<XTSocketResponse> message)
        {
            if (message.Data.Code != 0)
                return new CallResult<XTSocketResponse>(new ServerError(message.Data.Code, _client.GetErrorInfo(message.Data.Code, message.Data.Message)));

            return message.ToCallResult();
        }
    }
}
