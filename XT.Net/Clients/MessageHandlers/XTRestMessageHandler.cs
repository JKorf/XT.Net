using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Converters.SystemTextJson.MessageHandlers;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using XT.Net.Objects.Internal;

namespace XT.Net.Clients.MessageHandlers
{
    internal class XTRestMessageHandler : JsonRestMessageHandler
    {
        private readonly ErrorMapping _errorMapping;

        public override JsonSerializerOptions Options { get; } = SerializerOptions.WithConverters(XTExchange._serializerContext);

        public XTRestMessageHandler(ErrorMapping errorMapping)
        {
            _errorMapping = errorMapping;
        }

        public override Error? CheckDeserializedResponse<T>(HttpResponseHeaders responseHeaders, T result)
        {
            if (result is XTFuturesRestResponse xtFuturesResponse)
            {
                if (xtFuturesResponse.ReturnCode != 0)
                    return new ServerError(xtFuturesResponse.Error!.Code!, _errorMapping.GetErrorInfo(xtFuturesResponse.Error.Code!, xtFuturesResponse.Error.Message));
            }

            if (result is XTRestResponse xtSpotResponse)
            {
                if (xtSpotResponse.MessageCode != null && !xtSpotResponse.MessageCode.Equals("SUCCESS", StringComparison.Ordinal))
                    return new ServerError(xtSpotResponse.MessageCode, _errorMapping.GetErrorInfo(xtSpotResponse.MessageCode, null));
            }

            return null;
        }

        public override async ValueTask<Error> ParseErrorResponse(int httpStatusCode, HttpResponseHeaders responseHeaders, Stream responseStream)
        {
            if (httpStatusCode == 401)
                return new ServerError(new ErrorInfo(ErrorType.Unauthorized, "Unauthorized"));

            var (parseError, document) = await GetJsonDocument(responseStream).ConfigureAwait(false);
            if (parseError != null)
                return parseError;

            if (!document!.RootElement.TryGetProperty("error", out var errorProp))
                return new ServerError(ErrorInfo.Unknown);

            var code = errorProp.TryGetProperty("code", out var codeProp) ? codeProp.GetString() : null;
            var msg = errorProp.TryGetProperty("msg", out var msgProp) ? msgProp.GetString() : null;

            return new ServerError(code!, _errorMapping.GetErrorInfo(code!, msg));
        }
    }
}
