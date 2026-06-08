using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using XT.Net.Objects.Options;

namespace XT.Net.Clients
{
    /// <summary>
    /// Shared base for the XT spot and futures socket api clients. 
    /// Provides a per-API-key listen-key cache and a helper that force-refreshes the cached value before a resubscribe
    /// </summary>
    internal abstract class XTSocketApiClient<TAuthProvider> : SocketApiClient<XTEnvironment, TAuthProvider, XTCredentials>
        where TAuthProvider : AuthenticationProvider<XTCredentials>
    {
        // Conservative refresh interval. The documented TTL is longer, but server-side invalidations (e.g. another
        // ws-token call from the same key) can shorten it. Refreshing every ~25 min is cheap and covers external
        // invalidation cases.
        private static readonly TimeSpan _listenKeyLifetime = TimeSpan.FromMinutes(25);
        private static readonly TimeSpan _listenKeyRefreshSkew = TimeSpan.FromSeconds(5);

        private readonly ConcurrentDictionary<string, CachedListenKey> _listenKeyCache = new();

        /// <inheritdoc />
        protected XTSocketApiClient(ILogger logger, string baseAddress, XTSocketOptions options, SocketApiOptions apiOptions)
            : base(logger, baseAddress, options, apiOptions)
        {
        }

        /// <summary>
        /// Force-refresh the listen key from the REST endpoint and assign the new value to the subscription via the
        /// supplied delegate. Intended to be called from a subclass's <c>RevitalizeRequestAsync</c> override before
        /// each resubscribe.
        /// </summary>
        protected async Task<CallResult> RefreshSubscriptionListenKeyAsync(Action<string> assignListenKey)
        {
            var listenKey = await GetListenKeyAsync(forceRefresh: true).ConfigureAwait(false);
            if (!listenKey)
                return listenKey.AsDataless();

            assignListenKey(listenKey.Data);
            return CallResult.SuccessResult;
        }

        /// <summary>
        /// Get a listen key for the configured API credentials, returning the cached value when it is still valid
        /// and the caller did not request a force refresh.
        /// </summary>
        /// <param name="forceRefresh">Bypass the cache. Use after a reconnect, where the previous listen key has
        /// likely been invalidated server-side.</param>
        protected async Task<CallResult<string>> GetListenKeyAsync(bool forceRefresh = false)
        {
            if (ApiCredentials == null)
                return new CallResult<string>(new NoApiCredentialsError());

            var cacheKey = ApiCredentials.Key;
            if (!forceRefresh
                && _listenKeyCache.TryGetValue(cacheKey, out var cached)
                && cached.Expire > DateTime.UtcNow)
            {
                return new CallResult<string>(cached.ListenKey);
            }

            _logger.LogDebug("[Sckt] Requesting fresh XT listen key for {Client} (forceRefresh={ForceRefresh})", GetType().Name, forceRefresh);
            var result = await FetchListenKeyAsync().ConfigureAwait(false);
            if (result)
                _listenKeyCache[cacheKey] = new CachedListenKey { ListenKey = result.Data, Expire = DateTime.UtcNow + _listenKeyLifetime - _listenKeyRefreshSkew };
            else
                _logger.LogWarning("[Sckt] Failed to retrieve XT listen key for {Client}: {Error}", GetType().Name, result.Error);

            return result;
        }

        /// <summary>
        /// Subclass hook: call the REST endpoint that issues a websocket listen key for the configured API
        /// credentials. The base class handles caching and refresh-on-reconnect; the subclass only needs to know
        /// which endpoint to hit.
        /// </summary>
        protected abstract Task<CallResult<string>> FetchListenKeyAsync();

        private sealed class CachedListenKey
        {
            public string ListenKey { get; set; } = string.Empty;
            public DateTime Expire { get; set; }
        }
    }
}
