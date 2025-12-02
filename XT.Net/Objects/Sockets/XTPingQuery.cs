using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Objects.Sockets
{
    internal class XTPingQuery : Query<string>
    {
        public XTPingQuery() : base("ping", false, 0)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageMatcher = MessageMatcher.Create<string>("pong", HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutHandler<string>("pong");
        }

        public CallResult<string> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, string message)
        {
            return new CallResult<string>(message, originalData, null);
        }
    }
}
