using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;

namespace XT.Net.Objects.Sockets
{
    internal class XTPingQuery : Query<string>
    {
        public XTPingQuery() : base("ping", false, 0)
        {
            RequestTimeout = TimeSpan.FromSeconds(5);
            MessageRouter = MessageRouter.CreateVoid<string>("pong");
        }
    }
}
