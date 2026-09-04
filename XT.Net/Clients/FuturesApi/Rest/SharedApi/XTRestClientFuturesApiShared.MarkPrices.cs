using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using XT.Net.Interfaces.Clients.FuturesApi;
using System.Linq;
using CryptoExchange.Net.Objects;
using XT.Net.Enums;
using CryptoExchange.Net;
using XT.Net.Objects.Models;
using CryptoExchange.Net.Objects.Errors;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTRestClientFuturesSharedApi
    {
        #region Get Mark Prices

        async Task<ICallResult<SharedMarkPrice[]>> IGetMarkPrices.GetMarkPricesAsync(GetMarkPricesRequest request, CancellationToken ct)
            => await GetMarkPricesAsync(request, ct).ConfigureAwait(false);

        public GetMarkPricesOptions GetMarkPricesOptions { get; } = new GetMarkPricesOptions(_exchangeName, false);
        public async Task<HttpResult<SharedMarkPrice[]>> GetMarkPricesAsync(GetMarkPricesRequest request, CancellationToken ct)
        {
            var validationError = GetMarkPricesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedMarkPrice[]>(Exchange, validationError);

            var result = await _api.ExchangeData.GetMarkPricesAsync(ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedMarkPrice[]>(result);

            return HttpResult.Ok(result, result.Data.Select(d => 
                new SharedMarkPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, d.Symbol),
                    d.Symbol,
                    d.Price)).ToArray());
        }

        #endregion

        #region Get Mark Price

        async Task<ICallResult<SharedMarkPrice>> IGetMarkPrice.GetMarkPriceAsync(GetMarkPriceRequest request, CancellationToken ct)
            => await GetMarkPriceAsync(request, ct).ConfigureAwait(false);

        public GetMarkPriceOptions GetMarkPriceOptions { get; } = new GetMarkPriceOptions(_exchangeName, false);
        public async Task<HttpResult<SharedMarkPrice>> GetMarkPriceAsync(GetMarkPriceRequest request, CancellationToken ct)
        {
            var validationError = GetMarkPriceOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedMarkPrice>(Exchange, validationError);

            var result = await _api.ExchangeData.GetMarkPriceAsync(request.SymbolName(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedMarkPrice>(result);

            return HttpResult.Ok(result, 
                new SharedMarkPrice(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, result.Data.Symbol),
                    result.Data.Symbol,
                    result.Data.Price
                ));
        }

        #endregion
    }
}
