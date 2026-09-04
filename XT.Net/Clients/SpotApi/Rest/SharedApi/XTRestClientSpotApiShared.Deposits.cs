using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XT.Net.Clients.FuturesApi;
using XT.Net.Enums;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Models;

namespace XT.Net.Clients.SpotApi
{
    internal partial class XTRestClientSpotSharedApi
    {
        #region Get Deposit Addresses

        async Task<ICallResult<SharedDepositAddress[]>> IGetDepositAddresses.GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
            => await GetDepositAddressesAsync(request, ct).ConfigureAwait(false);

        public GetDepositAddressesOptions GetDepositAddressesOptions { get; } = new GetDepositAddressesOptions(_exchangeName, true)
        {
            RequiredRequestParameters = [
                RequestParameter<GetDepositAddressesRequest>.Required(x => x.Network,  "Network name", "Ethereum")
            ]
        };
        public async Task<HttpResult<SharedDepositAddress[]>> GetDepositAddressesAsync(GetDepositAddressesRequest request, CancellationToken ct)
        {
            var validationError = GetDepositAddressesOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDepositAddress[]>(Exchange, validationError);

            var depositAddresses = await _api.Account.GetDepositAddressAsync(request.Asset, request.Network!, ct: ct).ConfigureAwait(false);
            if (!depositAddresses.Success)
                return HttpResult.Fail<SharedDepositAddress[]>(depositAddresses);

            return HttpResult.Ok(depositAddresses, new[] { new SharedDepositAddress(request.Asset, depositAddresses.Data.Address)
            {
                TagOrMemo = depositAddresses.Data.Memo
            }
            });
        }

        #endregion

        #region Get Deposit History

        async Task<ICallResult<SharedDeposit[]>> IGetDepositHistory.GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => await GetDepositHistoryAsync(request, pageRequest, ct).ConfigureAwait(false);

        Task<HttpResult<SharedDeposit[]>> IDepositRestClient.GetDepositsAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetDepositHistoryAsync(request, pageRequest, ct);
        GetDepositHistoryOptions IDepositRestClient.GetDepositsOptions => GetDepositHistoryOptions;

        public GetDepositHistoryOptions GetDepositHistoryOptions { get; } = new GetDepositHistoryOptions(_exchangeName, false, true, true, 100)
        {
            RequiredRequestParameters = [
                RequestParameter<GetDepositsRequest>.Required(x => x.Asset,  "Asset filter for the withdrawals", "eth")
            ],
            RequiredExchangeParameters = [
            
                ExchangeParameterDescription.Required(
                    "Network",
                    aliases: ["chain"],
                    description: "Network filter for the withdrawals",
                    exampleValue: "Ethereum"
                    )
            ]
        };

        public async Task<HttpResult<SharedDeposit[]>> GetDepositHistoryAsync(GetDepositsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetDepositHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedDeposit[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetDepositHistoryAsync(
                request.Asset!,
                request.GetParamValue<string>(Exchange, "Network", "chain")!,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                direction: PageDirection.Previous,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedDeposit[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Data.Min(x => x.Id)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedDeposit(
                            x.Asset, 
                            x.Quantity,
                            x.Status == DepositStatus.Success,
                            x.CreateTime,
                            ParseTransferStatus(x.Status))
                        {
                            Confirmations = x.Confirmations,
                            Network = x.Network,
                            TransactionId = x.TransactionId,
                            Tag = x.Memo,
                            Id = x.Id.ToString()
                        })
                    .ToArray(), nextPageRequest);
        }

        #endregion

        private SharedTransferStatus ParseTransferStatus(DepositStatus status)
        {
            if (status == DepositStatus.Success)
                return SharedTransferStatus.Completed;

            if (status == DepositStatus.Fail || status == DepositStatus.Canceled)
                return SharedTransferStatus.Failed;

            if (status == DepositStatus.Submited
                || status == DepositStatus.Audited
                || status == DepositStatus.Review
                || status == DepositStatus.Pending)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
        }
    }
}
