using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTRestClientFuturesSharedApi
    {
        #region Get All Index Prices

        async Task<ICallResult<SharedIndexPrice[]>> IGetAllIndexPrices.GetAllIndexPricesAsync(GetAllIndexPricesRequest request, CancellationToken ct)
            => await GetAllIndexPricesAsync(request, ct).ConfigureAwait(false);

        public GetAllIndexPricesOptions GetAllIndexPricesOptions { get; } = new GetAllIndexPricesOptions(_exchangeName, false);
        public async Task<HttpResult<SharedIndexPrice[]>> GetAllIndexPricesAsync(GetAllIndexPricesRequest request, CancellationToken ct)
        {
            var validationError = GetAllIndexPricesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedIndexPrice[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetIndexPricesAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedIndexPrice[]>(result);

            return HttpResult.Ok(result, result.Data.Select(d => 
                new SharedIndexPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, d.Symbol),
                    d.Symbol, 
                    d.Price)).ToArray());
        }

        #endregion

        #region Get Index Price

        async Task<ICallResult<SharedIndexPrice>> IGetIndexPrice.GetIndexPriceAsync(GetIndexPriceRequest request, CancellationToken ct)
            => await GetIndexPriceAsync(request, ct).ConfigureAwait(false);

        public GetIndexPriceOptions GetIndexPriceOptions { get; } = new GetIndexPriceOptions(_exchangeName, false);
        public async Task<HttpResult<SharedIndexPrice>> GetIndexPriceAsync(GetIndexPriceRequest request, CancellationToken ct)
        {
            var validationError = GetIndexPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedIndexPrice>(Exchange, validationError);

            var result = await _api.ExchangeData.GetIndexPriceAsync(request.SymbolName(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedIndexPrice>(result);

            return HttpResult.Ok(result, new SharedIndexPrice(
                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                result.Data.Symbol,
                result.Data.Price));
        }

        #endregion
    }
}
