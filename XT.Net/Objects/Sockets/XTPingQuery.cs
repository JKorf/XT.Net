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
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public XTPingQuery() : base("ping", false, 0)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            ListenerIdentifiers = new HashSet<string> { "pong" };
        }

        public override CallResult<string> HandleMessage(SocketConnection connection, DataEvent<string> message)
        {
            return message.ToCallResult();
        }
    }
}
