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
using CryptoExchange.Net.Converters.SystemTextJson;

namespace XT.Net.Clients.FuturesApi
{
    internal partial class XTRestClientFuturesSharedApi
    {
        #region Get Ledger

        async Task<ICallResult<SharedLedgerEntry[]>> IGetLedger.GetLedgerAsync(GetLedgerRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetLedgerAsync(request, pageRequest, ct).ConfigureAwait(false);

        public GetLedgerOptions GetLedgerOptions { get; } = new GetLedgerOptions(_exchangeName, false, true, true, 100);

        public async Task<HttpResult<SharedLedgerEntry[]>> GetLedgerAsync(GetLedgerRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetLedgerOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedLedgerEntry[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);


            var result = await _api.Account.GetAccountBillsAsync(                
                limit: pageParams.Limit,
                direction: Enums.PageDirection.Next,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                id: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedLedgerEntry[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => result.Data.HasNext == false ? null : Pagination.NextPageFromId(result.Data.Data.Min(x => x.Id)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x =>
                        new SharedLedgerEntry(
                            x.Asset,
                            x.Quantity,
                            ParseEntryType(x.Type),
                            EnumConverter.GetString(x.Type),
                            x.CreateTime)
                        {
                            Id = x.Id.ToString()
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedLedgerEntryType ParseEntryType(BillType type)
        {
            if (type == BillType.Transfer)
                return SharedLedgerEntryType.Transfer;
            
            if (type == BillType.LiquidationManagementFee
                || type == BillType.Fee)
            {
                return SharedLedgerEntryType.Fee;
            }

            if (type == BillType.FundingFee)
                return SharedLedgerEntryType.FundingFee;

            return SharedLedgerEntryType.Unknown;
        }

        #endregion
    }
}
