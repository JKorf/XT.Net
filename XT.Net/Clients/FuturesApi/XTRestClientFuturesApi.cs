using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XT.Net.Interfaces.Clients.FuturesApi;
using XT.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Objects.Options;
using XT.Net.Objects.Internal;

namespace XT.Net.Clients.FuturesApi
{

    /// <inheritdoc cref="IXTRestClientFuturesApi" />
    internal class XTRestClientUsdtFuturesApi : XTRestClientFuturesApi
    {
        #region constructor/destructor
        internal XTRestClientUsdtFuturesApi(ILogger logger, XTRestClient baseClient, HttpClient? httpClient, XTRestOptions options)
            : base(logger, baseClient, httpClient, options.Environment.UsdtFuturesRestClientAddress, options, options.FuturesOptions)
        {
        }
        #endregion
    }

    /// <inheritdoc cref="IXTRestClientFuturesApi" />
    internal class XTRestClientCoinFuturesApi : XTRestClientFuturesApi
    {
        #region constructor/destructor
        internal XTRestClientCoinFuturesApi(ILogger logger, XTRestClient baseClient, HttpClient? httpClient, XTRestOptions options)
            : base(logger, baseClient, httpClient, options.Environment.CoinFuturesRestClientAddress, options, options.FuturesOptions)
        {
        }
        #endregion
    }

    /// <inheritdoc cref="IXTRestClientFuturesApi" />
    internal abstract partial class XTRestClientFuturesApi : RestApiClient, IXTRestClientFuturesApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Futures Api");
        internal XTRestClient _baseClient;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IXTRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IXTRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IXTRestClientFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "XT";
        #endregion

        #region constructor/destructor
        internal XTRestClientFuturesApi(ILogger logger, XTRestClient baseClient, HttpClient? httpClient, string baseAddress, XTRestOptions options, RestApiOptions apiOptions)
            : base(logger, httpClient, baseAddress, options, apiOptions)
        {
            _baseClient = baseClient;

            Account = new XTRestClientFuturesApiAccount(this);
            ExchangeData = new XTRestClientFuturesApiExchangeData(logger, this);
            Trading = new XTRestClientFuturesApiTrading(logger, this);

            RequestBodyEmptyContent = "";
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(XTExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTFuturesAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTFuturesRestResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.ReturnCode != 0)
                return result.AsDatalessError(new ServerError(result.Data.Error!.Code + ": " + result.Data.Error.Message));

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTFuturesRestResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            if (result.Data.ReturnCode != 0)
                return result.AsError<T>(new ServerError(result.Data.Error!.Code + ": " + result.Data.Error.Message));

            return result.As(result.Data.Result!);
        }

        internal Task<WebCallResult<T>> SendRawAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressRawAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressRawAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override TimeSyncInfo? GetTimeSyncInfo()
            => new TimeSyncInfo(_logger, ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp, ApiOptions.TimestampRecalculationInterval ?? ClientOptions.TimestampRecalculationInterval, _timeSyncState);

        /// <inheritdoc />
        public override TimeSpan? GetTimeOffset()
            => _timeSyncState.TimeOffset;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IXTRestClientFuturesApiShared SharedClient => this;

    }
}
