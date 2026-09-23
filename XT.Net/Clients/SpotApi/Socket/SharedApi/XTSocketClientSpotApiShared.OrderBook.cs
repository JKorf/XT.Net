using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XT.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using CryptoExchange.Net;
using System.Linq;

namespace XT.Net.Clients.SpotApi
{
    internal partial class XTSocketClientSpotSharedApi
    {
        #region Subscribe Order Book

        public SubscribeOrderBookOptions SubscribeOrderBookOptions { get; } = new SubscribeOrderBookOptions(_exchangeName, false, [5, 10, 20, 50])
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(SubscribeOrderBookRequest request, Action<DataEvent<SharedOrderBook>> handler, CancellationToken ct)
        {
            var validationError = SubscribeOrderBookOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToOrderBookUpdatesAsync(symbols, request.Limit ?? 10, update =>
            {
                handler(update.ToType(new SharedOrderBook(SharedQuantityType.BaseAsset, null, update.Data.Asks, update.Data.Bids)));
            }, ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
