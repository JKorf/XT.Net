using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using System;
using XT.Net.Objects.Options;

namespace XT.Net.Interfaces
{
    /// <summary>
    /// XT local order book factory
    /// </summary>
    public interface IXTOrderBookFactory
    {
        
        /// <summary>
        /// USDT futures order book factory methods
        /// </summary>
        IOrderBookFactory<XTOrderBookOptions> UsdtFutures { get; }

        /// <summary>
        /// Coin futures order book factory methods
        /// </summary>
        IOrderBookFactory<XTOrderBookOptions> CoinFutures { get; }

        /// <summary>
        /// Spot order book factory methods
        /// </summary>
        IOrderBookFactory<XTOrderBookOptions> Spot { get; }

        /// <summary>
        /// Create a SymbolOrderBook for the symbol
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="options">Book options</param>
        /// <returns></returns>
        ISymbolOrderBook Create(SharedSymbol symbol, Action<XTOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new USDT futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateUsdtFutures(string symbol, Action<XTOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Coin futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateCoinFutures(string symbol, Action<XTOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<XTOrderBookOptions>? options = null);

    }
}