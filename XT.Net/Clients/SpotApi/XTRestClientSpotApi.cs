using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XT.Net.Interfaces.Clients.SpotApi;
using XT.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using XT.Net.Objects.Internal;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using XT.Net.Clients.MessageHandlers;
using System.Net.Http.Headers;

namespace XT.Net.Clients.SpotApi
{
    /// <inheritdoc cref="IXTRestClientSpotApi" />
    internal partial class XTRestClientSpotApi : RestApiClient, IXTRestClientSpotApi
    {
        #region fields 
        internal static TimeSyncState _timeSyncState = new TimeSyncState("Spot Api");

        internal new XTSpotRestApiOptions ApiOptions => (XTSpotRestApiOptions)base.ApiOptions;
        internal new XTRestOptions ClientOptions => (XTRestOptions)base.ClientOptions;

        protected override ErrorMapping ErrorMapping => XTErrors.SpotErrors;
        protected override IRestMessageHandler MessageHandler { get; } = new XTRestMessageHandler(XTErrors.SpotErrors);
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IXTRestClientSpotApiAccount Account { get; }
        /// <inheritdoc />
        public IXTRestClientSpotApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IXTRestClientSpotApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "XT";
        #endregion

        #region constructor/destructor
        internal XTRestClientSpotApi(ILogger logger, HttpClient? httpClient, XTRestOptions options)
            : base(logger, httpClient, options.Environment.SpotRestClientAddress, options, options.SpotOptions)
        {
            Account = new XTRestClientSpotApiAccount(this);
            ExchangeData = new XTRestClientSpotApiExchangeData(logger, this);
            Trading = new XTRestClientSpotApiTrading(logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IStreamMessageAccessor CreateAccessor() => new SystemTextJsonStreamMessageAccessor(SerializerOptions.WithConverters(XTExchange._serializerContext));
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new XTSpotAuthenticationProvider(credentials);

        internal Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
            => SendToAddressAsync(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult> SendToAddressAsync(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTRestResponse>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            return result.AsDataless();
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) 
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) 
        {
            var result = await base.SendAsync<XTRestResponse<T>>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result)
                return result.As<T>(default);

            return result.As(result.Data.Result!);
        }

        protected override Error? TryParseError(RequestDefinition request, HttpResponseHeaders responseHeaders, IMessageAccessor accessor)
        {
            var msgCode = accessor.GetValue<string>(MessagePath.Get().Property("mc"));
            if (msgCode != null && msgCode != "SUCCESS")
                return new ServerError(msgCode, GetErrorInfo(msgCode));

            return null;
        }

        protected override Error ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, IMessageAccessor accessor, Exception? exception)
        {
            if (!accessor.IsValid)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            var code = accessor.GetValue<string?>(MessagePath.Get().Property("mc"));
            if (code == null)
                return new ServerError(ErrorInfo.Unknown, exception: exception);

            return new ServerError(code, GetErrorInfo(code), exception);
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
        public IXTRestClientSpotApiShared SharedClient => this;

    }
}
