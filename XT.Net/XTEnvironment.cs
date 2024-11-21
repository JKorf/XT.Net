using CryptoExchange.Net.Objects;
using XT.Net.Objects;

namespace XT.Net
{
    /// <summary>
    /// XT environments
    /// </summary>
    public class XTEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Spot rest API address
        /// </summary>
        public string SpotRestClientAddress { get; }
        /// <summary>
        /// USDT-M futures rest API address
        /// </summary>
        public string UsdtFuturesRestClientAddress { get; }
        /// <summary>
        /// Coin-M futures rest API address
        /// </summary>
        public string CoinFuturesRestClientAddress { get; }

        /// <summary>
        /// Socket API address
        /// </summary>
        public string SocketClientAddress { get; }

        internal XTEnvironment(
            string name,
            string spotRestAddress,
            string usdtFuturesRestAddress,
            string coinFuturesRestAddress,
            string streamAddress) :
            base(name)
        {
            SpotRestClientAddress = spotRestAddress;
            UsdtFuturesRestClientAddress = usdtFuturesRestAddress;
            CoinFuturesRestClientAddress = coinFuturesRestAddress;
            SocketClientAddress = streamAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public XTEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the XT environment by name
        /// </summary>
        public static XTEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Live environment
        /// </summary>
        public static XTEnvironment Live { get; }
            = new XTEnvironment(TradeEnvironmentNames.Live,
                                     XTApiAddresses.Default.SpotRestClientAddress,
                                     XTApiAddresses.Default.UsdtFuturesRestClientAddress,
                                     XTApiAddresses.Default.CoinFuturesRestClientAddress,
                                     XTApiAddresses.Default.SocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static XTEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string usdtFuturesRestAddress,
                        string coinFuturesRestAddress,
                        string spotSocketStreamsAddress)
            => new XTEnvironment(name, spotRestAddress, usdtFuturesRestAddress, coinFuturesRestAddress, spotSocketStreamsAddress);
    }
}
