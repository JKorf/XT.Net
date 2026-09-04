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
        #region Transfer

        async Task<ICallResult<SharedId>> ITransfer.TransferAsync(TransferRequest request, CancellationToken ct)
            => await TransferAsync(request, ct).ConfigureAwait(false);

        public TransferOptions TransferOptions { get; } = new TransferOptions(_exchangeName, [
            SharedAccountType.Spot,
            SharedAccountType.CrossMargin,
            SharedAccountType.IsolatedMargin,
            SharedAccountType.PerpetualLinearFutures,
            SharedAccountType.PerpetualInverseFutures,
            SharedAccountType.DeliveryLinearFutures,
            SharedAccountType.DeliveryInverseFutures
            ]);
        public async Task<HttpResult<SharedId>> TransferAsync(TransferRequest request, CancellationToken ct)
        {
            var validationError = TransferOptions.ValidateRequest(request, this);
            if (validationError != null)
                return HttpResult.Fail<SharedId>(Exchange, validationError);

            var fromType = GetTransferType(request.FromAccountType);
            var toType = GetTransferType(request.ToAccountType);
            if (fromType == null || toType == null)
                return HttpResult.Fail<SharedId>(Exchange, ArgumentError.Invalid("To/From AccountType", "invalid to/from account combination"));

            // Get data
            var transfer = await _api.Account.TransferAsync(
                request.Asset,
                fromType.Value,
                toType.Value,
                request.Quantity,
                Guid.NewGuid().ToString(),
                ct: ct).ConfigureAwait(false);
            if (!transfer.Success)
                return HttpResult.Fail<SharedId>(transfer);

            return HttpResult.Ok(transfer, new SharedId(""));
        }

        #endregion

        private BusinessType? GetTransferType(SharedAccountType type)
        {
            if (type == SharedAccountType.Spot) return BusinessType.Spot;
            if (type.IsMarginAccount()) return BusinessType.Leverage;
            if (type == SharedAccountType.PerpetualLinearFutures || type == SharedAccountType.DeliveryLinearFutures) return BusinessType.UsdtFutures;
            if (type == SharedAccountType.PerpetualInverseFutures || type == SharedAccountType.DeliveryInverseFutures) return BusinessType.CoinFutures;
            return null;
        }
    }
}
