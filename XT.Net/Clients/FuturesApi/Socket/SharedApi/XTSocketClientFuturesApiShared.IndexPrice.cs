using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XT.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using CryptoExchange.Net;
using System.Linq;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTSocketClientFuturesSharedApi
    {
        #region Subscribe Index Price

        public SubscribeIndexPriceOptions SubscribeIndexPriceOptions { get; } = new SubscribeIndexPriceOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToIndexPriceUpdatesAsync(SubscribeIndexPriceRequest request, Action<DataEvent<SharedIndexPrice>> handler, CancellationToken ct)
        {
            var validationError = SubscribeIndexPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToIndexPriceUpdatesAsync(symbols, update => handler(update.ToType<SharedIndexPrice>(
                new SharedIndexPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    update.Data.Price)
            )), ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
