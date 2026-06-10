using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
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
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Errors;
using XT.Net.Clients.MessageHandlers;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using System.Net.Http.Headers;

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
    internal abstract partial class XTRestClientFuturesApi : RestApiClient<XTEnvironment, XTFuturesAuthenticationProvider, XTCredentials>, IXTRestClientFuturesApi
    {
        #region fields 
        internal XTRestClient _baseClient;

        protected override ErrorMapping ErrorMapping => XTErrors.FuturesErrors;
        internal new XTRestOptions ClientOptions => (XTRestOptions)base.ClientOptions;
        protected override IRestMessageHandler MessageHandler { get; } = new XTRestMessageHandler(XTErrors.FuturesErrors);
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
            : base(logger, XTExchange.Metadata.Id, httpClient, baseAddress, options, apiOptions)
        {
            _baseClient = baseClient;

            Account = new XTRestClientFuturesApiAccount(this);
            ExchangeData = new XTRestClientFuturesApiExchangeData(logger, this);
            Trading = new XTRestClientFuturesApiTrading(logger, this);

            RequestBodyEmptyContent = "";
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(SerializerOptions.WithConverters(XTExchange._serializerContext));

        /// <inheritdoc />
        protected override XTFuturesAuthenticationProvider CreateAuthenticationProvider(XTCredentials credentials)
            => new XTFuturesAuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTFuturesRestResponse>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return HttpResult.Ok(result);
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<XTFuturesRestResponse<T>>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<T>(result);

            return HttpResult.Ok(result, result.Data.Result!);
        }

        internal async Task<HttpResult<T>> SendRawAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            return await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => XTExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IXTRestClientFuturesApiShared SharedClient => this;

    }
}
