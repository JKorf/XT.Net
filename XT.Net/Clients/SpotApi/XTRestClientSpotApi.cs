using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
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
    internal partial class XTRestClientSpotApi : RestApiClient<XTEnvironment, XTSpotAuthenticationProvider, XTCredentials>, IXTRestClientSpotApi
    {
        #region fields 
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
        internal XTRestClientSpotApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, XTRestOptions options)
            : base(loggerFactory, XTExchange.Metadata.Id, httpClient, options.Environment.SpotRestClientAddress, options, options.SpotOptions)
        {
            Account = new XTRestClientSpotApiAccount(this);
            ExchangeData = new XTRestClientSpotApiExchangeData(_logger, this);
            Trading = new XTRestClientSpotApiTrading(_logger, this);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        /// <inheritdoc />
        protected override XTSpotAuthenticationProvider CreateAuthenticationProvider(XTCredentials credentials)
            => new XTSpotAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTRestResponse>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return HttpResult.Ok(result);
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) 
        {
            var result = await base.SendAsync<XTRestResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            return HttpResult.Ok(result, result.Data.Result!);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IXTRestClientSpotApiShared SharedClient => this;

    }
}
