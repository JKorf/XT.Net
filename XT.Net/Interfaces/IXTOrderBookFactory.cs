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
        /// Futures order book factory methods
        /// </summary>
        IOrderBookFactory<XTOrderBookOptions> Futures { get; }

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
        /// Create a new Futures local order book instance
        /// </summary>
        ISymbolOrderBook CreateFutures(string symbol, Action<XTOrderBookOptions>? options = null);

        /// <summary>
        /// Create a new Spot local order book instance
        /// </summary>
        ISymbolOrderBook CreateSpot(string symbol, Action<XTOrderBookOptions>? options = null);

    }
}