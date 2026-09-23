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
        #region Subscribe Mark Price

        public SubscribeMarkPriceOptions SubscribeMarkPriceOptions { get; } = new SubscribeMarkPriceOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(SubscribeMarkPriceRequest request, Action<DataEvent<SharedMarkPrice>> handler, CancellationToken ct)
        {
            var validationError = SubscribeMarkPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToMarkPriceUpdatesAsync(symbols, update => handler(update.ToType<SharedMarkPrice>(
                new SharedMarkPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                    update.Data.Symbol,
                    update.Data.Price)
            )), ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
