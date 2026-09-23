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
        #region Subscribe Trades

        public SubscribeTradeOptions SubscribeTradeOptions { get; } = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToTradeUpdatesAsync(symbols,
                update =>
                {
                    handler(update.ToType<SharedTrade[]>([
                        new SharedTrade(
                            ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                            update.Data.Symbol,
                            new SharedOrderQuantity(update.Data.Quantity), 
                            update.Data.Price, 
                            update.Data.Timestamp)
                    {
                        Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
                    }]));
                }, ct).ConfigureAwait(false);

            return result;
        }

        #endregion
    }
}
