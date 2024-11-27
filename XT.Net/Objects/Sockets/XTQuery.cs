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
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public XTQuery(XTSocketRequest request, params string[] additionalIdentifiers) : base(request, false, 1)
        {
            ListenerIdentifiers = new HashSet<string> { request.Id };
            foreach (string identifier in additionalIdentifiers)
                ListenerIdentifiers.Add(identifier);
        }

        public override CallResult<object> Deserialize(IMessageAccessor message, Type type)
        {
            if (!message.IsJson)
                return new CallResult<object>(new XTSocketResponse() { Code = -1, Message = "Invalid listen key" });

            return base.Deserialize(message, type);
        }

        public override CallResult<XTSocketResponse> HandleMessage(SocketConnection connection, DataEvent<XTSocketResponse> message)
        {
            if (message.Data.Code != 0)
                return new CallResult<XTSocketResponse>(new ServerError(message.Data.Code, message.Data.Message));

            return message.ToCallResult();
        }
    }
}
