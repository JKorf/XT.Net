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
            MessageMatcher.Create<string>("pong", HandleMessage);
        }

        public CallResult<string> HandleMessage(SocketConnection connection, DataEvent<string> message)
        {
            return message.ToCallResult();
        }
    }
}
