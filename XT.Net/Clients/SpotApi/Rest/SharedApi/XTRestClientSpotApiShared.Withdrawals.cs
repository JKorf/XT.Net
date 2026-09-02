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
        #region Withdrawal client

        Task<HttpResult<SharedWithdrawal[]>> IWithdrawalRestClient.GetWithdrawalsAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
            => GetWithdrawalHistoryAsync(request, pageRequest, ct);
        GetWithdrawalHistoryOptions IWithdrawalRestClient.GetWithdrawalsOptions => GetWithdrawalHistoryOptions;

        public GetWithdrawalHistoryOptions GetWithdrawalHistoryOptions { get; } = new GetWithdrawalHistoryOptions(_exchangeName, false, true, true, 200)
        {
            RequiredRequestParameters = [
                RequestParameter<GetWithdrawalsRequest>.Required(x => x.Asset,  "Asset filter for the withdrawals", "eth")
            ],
            RequiredExchangeParameters = new List<ParameterDescription>
            {
                ExchangeParameterDescription.Required(
                    "Network",
                    aliases: ["chain"],
                    description: "Network filter for the withdrawals",
                    exampleValue: "Ethereum"
                    )
            }
        };
        public async Task<HttpResult<SharedWithdrawal[]>> GetWithdrawalHistoryAsync(GetWithdrawalsRequest request, PageRequest? pageRequest, CancellationToken ct)
        {
            var validationError = GetWithdrawalHistoryOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedWithdrawal[]>(Exchange, validationError);

            int limit = request.Limit ?? 100;
            var direction = DataDirection.Descending;
            var pageParams = Pagination.GetPaginationParameters(direction, limit, request.StartTime, request.EndTime ?? DateTime.UtcNow, pageRequest, false);

            // Get data
            var result = await _api.Account.GetWithdrawalHistoryAsync(
                request.Asset!,
                request.GetParamValue<string>(Exchange, "Network", "chain")!,
                startTime: pageParams.StartTime,
                endTime: pageParams.EndTime,
                limit: pageParams.Limit,
                fromId: pageParams.FromId == null ? null : long.Parse(pageParams.FromId),
                direction: PageDirection.Next,
                ct: ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<SharedWithdrawal[]>(result);

            var nextPageRequest = Pagination.GetNextPageRequest(
                     () => Pagination.NextPageFromId(result.Data.Data.Min(x => x.Id)),
                     result.Data.Data.Length,
                     result.Data.Data.Select(x => x.CreateTime),
                     request.StartTime,
                     request.EndTime ?? DateTime.UtcNow,
                     pageParams);

            return HttpResult.Ok(result, ExchangeHelpers.ApplyFilter(result.Data.Data, x => x.CreateTime, request.StartTime, request.EndTime, direction)
                    .Select(x => 
                        new SharedWithdrawal(
                            x.Asset, 
                            x.Address,
                            x.Quantity,
                            x.Status == WithdrawalStatus.Success,
                            x.CreateTime,
                            GetWithdrawalStatus(x))
                        {
                            Confirmations = x.Confirmations,
                            Network = x.Network,
                            Tag = x.Memo,
                            TransactionId = x.TransactionId,
                            Fee = x.Fee,
                            Id = x.Id.ToString()
                        })
                    .ToArray(), nextPageRequest);
        }

        private SharedTransferStatus GetWithdrawalStatus(XTWithdrawal x)
        {
            if (x.Status == WithdrawalStatus.Canceled || x.Status == WithdrawalStatus.Fail)
                return SharedTransferStatus.Failed;

            if (x.Status == WithdrawalStatus.Success)
                return SharedTransferStatus.Completed;

            if (x.Status == WithdrawalStatus.Audited
                || x.Status == WithdrawalStatus.AuditedAgain
                || x.Status == WithdrawalStatus.InReview
                || x.Status == WithdrawalStatus.Pending
                || x.Status == WithdrawalStatus.Submited)
            {
                return SharedTransferStatus.InProgress;
            }

            return SharedTransferStatus.Unknown;
        }
        #endregion

        #region Withdraw client

        public WithdrawOptions WithdrawOptions { get; } = new WithdrawOptions(_exchangeName)
        {
            RequiredRequestParameters = [
                RequestParameter<WithdrawRequest>.Required(x => x.Network, "Network for the withdrawal", "Ethereum")
                ]
        };
        public async Task<HttpResult<SharedId>> WithdrawAsync(WithdrawRequest request, CancellationToken ct)
        {
            var validationError = WithdrawOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            // Get data
            var withdrawal = await _api.Account.WithdrawAsync(
                request.Asset,
                request.Network!,
                request.Address,
                request.Quantity,
                memo: request.AddressTag,
                ct: ct).ConfigureAwait(false);
            if (!withdrawal.Success)
                return HttpResult.Fail<SharedId>(withdrawal);

            return HttpResult.Ok(withdrawal, new SharedId(withdrawal.Data.Id.ToString()));
        }

        #endregion
    }
}
