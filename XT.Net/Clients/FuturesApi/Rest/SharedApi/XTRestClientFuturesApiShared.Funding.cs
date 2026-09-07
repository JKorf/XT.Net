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
        #region Get Funding Rate History

        async Task<ICallResult<SharedFundingRate[]>> IGetFundingRateHistory.GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetFundingRateHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetFundingRateHistoryOptions GetFundingRateHistoryOptions { get; } = new GetFundingRateHistoryOptions(_exchangeName, false, true, false, 100, false);
        public async Task<HttpResult<SharedFundingRate[]>> GetFundingRateHistoryAsync(GetFundingRateHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetFundingRateHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingRate[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.ExchangeData.GetFundingRateHistoryAsync(
                request.Symbol!.GetSymbol(FormatSymbol),
                limit: pageParams.Limit,
                direction: Enums.PageDirection.Next,
                fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingRate[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Data.Min(x => x.Id)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.Timestamp),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.Timestamp, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedFundingRate(x.FundingRate, x.Timestamp))
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Get User Funding History

        async Task<ICallResult<SharedFundingFee[]>> IGetUserFundingHistory.GetUserFundingHistoryAsync(GetUserFundingHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetUserFundingHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetUserFundingHistoryOptions GetUserFundingHistoryOptions { get; } = new GetUserFundingHistoryOptions(_exchangeName, false, true, true, 100, false);
        public async Task<HttpResult<SharedFundingFee[]>> GetUserFundingHistoryAsync(GetUserFundingHistoryRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetUserFundingHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingFee[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetFundingFeeHistoryAsync(
                request.Symbol?.GetSymbol(FormatSymbol),
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                direction: Enums.PageDirection.Next,                
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingFee[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => result.Data.HasNext == false ? null : Pagination.NextPageFromTime(pageParams, result.Data.Data.Min(x => x.CreateTime)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedFundingFee(
                            x.Symbol,
                            x.FundingFee,
                            x.CreateTime
                            )
                        {
                            Asset = x.Asset,
                            Side = x.PositionSide == PositionSide.Short ? SharedPositionSide.Short: SharedPositionSide.Long,
                            Id = x.Id.ToString()
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        #region Get Funding Info

        async Task<ICallResult<SharedFundingInfo>> IGetFundingInfo.GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
            => await GetFundingInfoAsync(request, ct).ConfigureAwait(false);

        public GetFundingInfoOptions GetFundingInfoOptions { get; } = new GetFundingInfoOptions(_exchangeName, false, true, true, 100, false);
        public async Task<HttpResult<SharedFundingInfo>> GetFundingInfoAsync(GetFundingInfoRequest request, CancellationToken ct)
        {
            var validationError = GetFundingInfoOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedFundingInfo>(Exchange, validationError);

            var result = await _api.ExchangeData.GetFundingRateAsync(request.SymbolName(FormatSymbol), ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedFundingInfo>(result);

            return HttpResult.Ok(result,
                new SharedFundingInfo(
                    result.Data.LastFundingRate,
                    result.Data.NextFundingTime,
                    result.Data.FundingRateInterval
                ));
        }

        #endregion
    }
}
