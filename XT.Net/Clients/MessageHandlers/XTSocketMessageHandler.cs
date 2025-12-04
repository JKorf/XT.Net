using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text.Json;
using XT.Net;
using XT.Net.Objects.Internal;
using XT.Net.Objects.Models;

namespace Toobit.Net.Clients.MessageHandlers
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

        protected override MessageEvaluator[] TypeEvaluators { get; } = [ 
            new MessageEvaluator {
                Priority = 1,
                Fields = [
                    new PropertyFieldReference("id"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("id")!
            },

            new MessageEvaluator {
                Priority = 2,
                Fields = [
                    new PropertyFieldReference("topic"),
                ],
                IdentifyMessageCallback = x => x.FieldValue("topic")!
            },

        ];

        public override string? GetTypeIdentifier(ReadOnlySpan<byte> data, WebSocketMessageType? webSocketMessageType)
        {
            if (data.Length == 4)
                return "pong";

            return base.GetTypeIdentifier(data, webSocketMessageType);
        }
    }
}
