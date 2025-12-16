using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using System;

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
