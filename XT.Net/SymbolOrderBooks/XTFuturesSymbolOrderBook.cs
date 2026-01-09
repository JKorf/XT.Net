using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.OrderBook;
using Microsoft.Extensions.Logging;
using XT.Net.Clients;
using XT.Net.Interfaces.Clients;
using XT.Net.Objects.Models;
using XT.Net.Objects.Options;

namespace XT.Net.SymbolOrderBooks
{
    /// <summary>
    /// Implementation for a synchronized order book. After calling Start the order book will sync itself and keep up to date with new data. It will automatically try to reconnect and resync in case of a lost/interrupted connection.
    /// Make sure to check the State property to see if the order book is synced.
    /// </summary>
    public abstract class XTFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly bool _clientOwner;
        /// <summary>
        /// Rest client
        /// </summary>
        protected readonly IXTRestClient _restClient;
        private readonly IXTSocketClient _socketClient;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public XTFuturesSymbolOrderBook(string symbol, Action<XTOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null, null)
        {
            _clientOwner = true;
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="restClient">Rest client instance</param>
        /// <param name="socketClient">Socket client instance</param>
        public XTFuturesSymbolOrderBook(
            string symbol,
            Action<XTOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IXTRestClient? restClient,
            IXTSocketClient? socketClient) : base(logger, "XT", "Futures", symbol)
        {
            var options = XTOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _strictLevels = false;
            _sequencesAreConsecutive = options?.Limit == null;

            Levels = options?.Limit;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);
            _clientOwner = socketClient == null;
            _socketClient = socketClient ?? new XTSocketClient();
            _restClient = restClient ?? new XTRestClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            CallResult<UpdateSubscription> subResult;
            if (Levels == null)
                subResult = await _socketClient.FuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync(Symbol, 100, HandleUpdate).ConfigureAwait(false);
            else
                subResult = await _socketClient.FuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, Levels.Value, 100, HandleUpdate).ConfigureAwait(false);

            if (!subResult)
                return new CallResult<UpdateSubscription>(subResult.Error!);

            if (ct.IsCancellationRequested)
            {
                await subResult.Data.CloseAsync().ConfigureAwait(false);
                return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
            }

            Status = OrderBookStatus.Syncing;
            if (Levels == null)
            {
                // Small delay to make sure the snapshot is from after our first stream update
                await Task.Delay(200).ConfigureAwait(false);
                var bookResult = await GetOrderBookAsync().ConfigureAwait(false);
                if (!bookResult)
                {
                    _logger.Log(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Api} order book {Symbol} failed to retrieve initial order book");
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(bookResult.Error!);
                }

                SetSnapshot(bookResult.Data.UpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            }
            else
            {
                var setResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
            }

            return new CallResult<UpdateSubscription>(subResult.Data);
        }

        /// <summary>
        /// Get the order book snapshot
        /// </summary>
        /// <returns></returns>
        protected abstract Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync();

        private void HandleUpdate(DataEvent<XTFuturesIncrementalOrderBookUpdate> data)
        {
            UpdateOrderBook(data.Data.FirstUpdateId, data.Data.LastUpdateId, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        private void HandleUpdate(DataEvent<XTFuturesOrderBookUpdate> data)
        {
            SetSnapshot(data.Data.Timestamp.Ticks, data.Data.Bids, data.Data.Asks, data.DataTime, data.DataTimeLocal);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (Levels != null)
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);

            // Small delay to make sure the snapshot is from after our first stream update
            await Task.Delay(200).ConfigureAwait(false);
            var bookResult = await GetOrderBookAsync().ConfigureAwait(false);
            if (!bookResult)
                return new CallResult<bool>(bookResult.Error!);

            SetSnapshot(bookResult.Data.UpdateId, bookResult.Data.Bids, bookResult.Data.Asks);
            return new CallResult<bool>(true);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (_clientOwner)
            {
                _restClient?.Dispose();
                _socketClient?.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    /// <inheritdoc />
    public class XTUsdtFuturesSymbolOrderBook : XTFuturesSymbolOrderBook
    {
        /// <inheritdoc />
        public XTUsdtFuturesSymbolOrderBook(string symbol, Action<XTOrderBookOptions>? optionsDelegate = null) : base(symbol, optionsDelegate)
        {
        }

        /// <inheritdoc />
        public XTUsdtFuturesSymbolOrderBook(string symbol, Action<XTOrderBookOptions>? optionsDelegate, ILoggerFactory? logger, IXTRestClient? restClient, IXTSocketClient? socketClient) : base(symbol, optionsDelegate, logger, restClient, socketClient)
        {
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync()
        {
            return _restClient.UsdtFuturesApi.ExchangeData.GetOrderBookAsync(Symbol, 1000);
        }
    }

    /// <inheritdoc />
    public class XTCoinFuturesSymbolOrderBook : XTFuturesSymbolOrderBook
    {
        /// <inheritdoc />
        public XTCoinFuturesSymbolOrderBook(string symbol, Action<XTOrderBookOptions>? optionsDelegate = null) : base(symbol, optionsDelegate)
        {
        }

        /// <inheritdoc />
        public XTCoinFuturesSymbolOrderBook(string symbol, Action<XTOrderBookOptions>? optionsDelegate, ILoggerFactory? logger, IXTRestClient? restClient, IXTSocketClient? socketClient) : base(symbol, optionsDelegate, logger, restClient, socketClient)
        {
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<XTFuturesOrderBook>> GetOrderBookAsync()
        {
            return _restClient.CoinFuturesApi.ExchangeData.GetOrderBookAsync(Symbol, 1000);
        }
    }
}
