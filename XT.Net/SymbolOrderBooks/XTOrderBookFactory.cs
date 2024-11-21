using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using XT.Net.Interfaces;
using XT.Net.Interfaces.Clients;
using XT.Net.Objects.Options;

namespace XT.Net.SymbolOrderBooks
{
    /// <summary>
    /// XT order book factory
    /// </summary>
    public class XTOrderBookFactory : IXTOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public XTOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;            
            
            Futures = new OrderBookFactory<XTOrderBookOptions>(CreateFutures, Create);
            Spot = new OrderBookFactory<XTOrderBookOptions>(CreateSpot, Create);
        }
                
         /// <inheritdoc />
        public IOrderBookFactory<XTOrderBookOptions> Futures { get; }

         /// <inheritdoc />
        public IOrderBookFactory<XTOrderBookOptions> Spot { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<XTOrderBookOptions>? options = null)
        {
            var symbolName = XTExchange.FormatSymbol(symbol.BaseAsset, symbol.QuoteAsset, symbol.TradingMode, symbol.DeliverTime);

            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            return CreateFutures(symbolName, options);
        }

        
         /// <inheritdoc />
        public ISymbolOrderBook CreateFutures(string symbol, Action<XTOrderBookOptions>? options = null)
            => new XTFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IXTRestClient>(),
                                                          _serviceProvider.GetRequiredService<IXTSocketClient>());

         /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<XTOrderBookOptions>? options = null)
            => new XTSpotSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IXTRestClient>(),
                                                          _serviceProvider.GetRequiredService<IXTSocketClient>());


    }
}
