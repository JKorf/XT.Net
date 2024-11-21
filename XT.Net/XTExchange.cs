using CryptoExchange.Net.Objects;
using CryptoExchange.Net.RateLimiting.Filters;
using CryptoExchange.Net.RateLimiting.Guards;
using CryptoExchange.Net.RateLimiting.Interfaces;
using CryptoExchange.Net.RateLimiting;
using System;
using System.Collections.Generic;
using System.Text;
using CryptoExchange.Net.SharedApis;

namespace XT.Net
{
    /// <summary>
    /// XT exchange information and configuration
    /// </summary>
    public static class XTExchange
    {
        /// <summary>
        /// Exchange name
        /// </summary>
        public static string ExchangeName => "XT";

        /// <summary>
        /// Exchange display name
        /// </summary>
        public static string DisplayName { get; } = "XT";

        /// <summary>
        /// Url to exchange image
        /// </summary>
        public static string ImageUrl { get; } = "https://raw.githubusercontent.com/JKorf/XT.Net/master/XT.Net/Icon/icon.png";

        /// <summary>
        /// Url to the main website
        /// </summary>
        public static string Url { get; } = "https://www.xt.com";

        /// <summary>
        /// Urls to the API documentation
        /// </summary>
        public static string[] ApiDocsUrl { get; } = new[] {
            "https://doc.xt.com/"
            };

        /// <summary>
        /// Format a base and quote asset to an XT recognized symbol 
        /// </summary>
        /// <param name="baseAsset">Base asset</param>
        /// <param name="quoteAsset">Quote asset</param>
        /// <param name="tradingMode">Trading mode</param>
        /// <param name="deliverTime">Delivery time for delivery futures</param>
        /// <returns></returns>
        public static string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverTime = null)
        {

#warning spot
            return baseAsset.ToLower() + "_" + quoteAsset.ToLower();
        }

        /// <summary>
        /// Rate limiter configuration for the XT API
        /// </summary>
        public static XTRateLimiters RateLimiter { get; } = new XTRateLimiters();
    }

    /// <summary>
    /// Rate limiter configuration for the XT API
    /// </summary>
    public class XTRateLimiters
    {
        /// <summary>
        /// Event for when a rate limit is triggered
        /// </summary>
        public event Action<RateLimitEvent> RateLimitTriggered;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        internal XTRateLimiters()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            Initialize();
        }

        private void Initialize()
        {
            XT = new RateLimitGate("XT");
            XT.RateLimitTriggered += (x) => RateLimitTriggered?.Invoke(x);
        }


        internal IRateLimitGate XT { get; private set; }

    }
}
