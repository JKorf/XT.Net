using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Objects.Sockets
{
    internal class XTQuery : Query<XTSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public XTQuery(XTSocketRequest request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { request.Id };
        }

        public override CallResult<XTSocketResponse> HandleMessage(SocketConnection connection, DataEvent<XTSocketResponse> message)
        {
            if (message.Data.Code != 0)
                return new CallResult<XTSocketResponse>(new ServerError(message.Data.Code, message.Data.Message));

            return message.ToCallResult();
        }
    }
}
