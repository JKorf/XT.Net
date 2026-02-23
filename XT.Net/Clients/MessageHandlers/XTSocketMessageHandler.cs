using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.MessageHandlers
{
    internal class XTSocketMessageHandler : JsonSocketMessageHandler
    {
        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(XTExchange._serializerContext);

        public XTSocketMessageHandler()
        {
            AddTopicMapping<XTSocketUpdate<XTTradeUpdate>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTKlineUpdate>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XTOrderBookUpdate>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XTIncrementalOrderBookUpdate>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XT24HTicker>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTFuturesTrade>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTFuturesKline>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XTFuturesTicker>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTMarketInfo>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTPrice>>(x => x.Data.Symbol);
            AddTopicMapping<XTSocketUpdate<XTFuturesIncrementalOrderBookUpdate>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XTFuturesOrderBookUpdate>>(x => x.Event.Split('@')[1]);
            AddTopicMapping<XTSocketUpdate<XTFundingRateUpdate>>(x => x.Data.Symbol);
        }

        protected override MessageTypeDefinition[] TypeEvaluators { get; } = [ 
            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("id")!
            },

            new MessageTypeDefinition {
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                TypeIdentifierCallback = x => x.FieldValue("topic")!
            },

        ];

        protected override string? GetTypeIdentifierNonJson(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        {
            if (data.Length == 4)
                return "pong";

            // Invalid json, need to handle this for invalid listen key error response:
            // `balance@invalid_listen_key`
#if NETSTANDARD2_0
            return Encoding.UTF8.GetString(data.ToArray());
#else
            return Encoding.UTF8.GetString(data);
#endif
        }
    }
}
