using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using System;
using System.Collections.Generic;
using XT.Net.Objects.Internal;

namespace XT.Net.Objects.Sockets
{
    internal class XTQuery : Query<XTSocketResponse>
    {
        private readonly SocketApiClient _client;

        public XTQuery(SocketApiClient client, XTSocketRequest request, string? listenKeyTopic) : base(request, false, 1)
        {
            _client = client;
            var routes = new List<MessageRoute>();

            routes.Add(MessageRoute.CreateForQuery<XTSocketResponse>(request.Id, HandleMessage));

            if (listenKeyTopic != null)
                routes.Add(MessageRoute.CreateForQuery<string>($"{listenKeyTopic}@invalid_listen_key", HandleListenKeyError));
            
            MessageRouter = MessageRouter.Create(routes.ToArray());
        }

        public CallResult<string> HandleListenKeyError(SocketConnection connection, DateTime receiveTime, string? originalData, string message)
        {
            return CallResult.Fail<string>(new ServerError(new ErrorInfo(ErrorType.Unauthorized, "Invalid listen key")));
        }

        public CallResult<XTSocketResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, XTSocketResponse message)
        {
            if (message.Code != 0)
                return CallResult.Fail<XTSocketResponse>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message)));

            return CallResult.Ok<XTSocketResponse>(message, originalData);
        }
    }
}
