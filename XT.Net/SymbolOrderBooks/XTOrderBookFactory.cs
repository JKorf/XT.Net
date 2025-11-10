using CryptoExchange.Net;
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

        /// <inheritdoc />
        public string ExchangeName => XTExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public XTOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;            
            
            UsdtFutures = new OrderBookFactory<XTOrderBookOptions>(CreateUsdtFutures, Create);
            CoinFutures = new OrderBookFactory<XTOrderBookOptions>(CreateCoinFutures, Create);
            Spot = new OrderBookFactory<XTOrderBookOptions>(CreateSpot, Create);
        }
                
         /// <inheritdoc />
        public IOrderBookFactory<XTOrderBookOptions> UsdtFutures { get; }
        /// <inheritdoc />
        public IOrderBookFactory<XTOrderBookOptions> CoinFutures { get; }

        /// <inheritdoc />
        public IOrderBookFactory<XTOrderBookOptions> Spot { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<XTOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(XTExchange.FormatSymbol);

            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);

            if (symbol.TradingMode.IsLinear())
                return CreateUsdtFutures(symbolName, options);

            return CreateCoinFutures(symbolName, options);
        }

        
         /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<XTOrderBookOptions>? options = null)
            => new XTUsdtFuturesSymbolOrderBook(symbol, options, 
                                                          _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                                          _serviceProvider.GetRequiredService<IXTRestClient>(),
                                                          _serviceProvider.GetRequiredService<IXTSocketClient>());

        /// <inheritdoc />
        public ISymbolOrderBook CreateCoinFutures(string symbol, Action<XTOrderBookOptions>? options = null)
            => new XTCoinFuturesSymbolOrderBook(symbol, options,
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
