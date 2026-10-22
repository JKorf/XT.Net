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
        /// General REST API address
        /// </summary>
        public string RestClientAddress { get; }
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
        /// Socket spot API address
        /// </summary>
        public string SpotSocketClientAddress { get; }

        /// <summary>
        /// Socket futures API address
        /// </summary>
        public string FuturesSocketClientAddress { get; }

        internal XTEnvironment(
            string name,
            string spotRestAddress,
            string usdtFuturesRestAddress,
            string coinFuturesRestAddress,
            string spotStreamAddress,
            string futuresStreamAddress,
            string restAddress) :
            base(name)
        {
            RestClientAddress = restAddress;
            SpotRestClientAddress = spotRestAddress;
            UsdtFuturesRestClientAddress = usdtFuturesRestAddress;
            CoinFuturesRestClientAddress = coinFuturesRestAddress;
            SpotSocketClientAddress = spotStreamAddress;
            FuturesSocketClientAddress = futuresStreamAddress;
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
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static XTEnvironment Live { get; }
            = new XTEnvironment(TradeEnvironmentNames.Live,
                                     XTApiAddresses.Default.SpotRestClientAddress,
                                     XTApiAddresses.Default.UsdtFuturesRestClientAddress,
                                     XTApiAddresses.Default.CoinFuturesRestClientAddress,
                                     XTApiAddresses.Default.SpotSocketClientAddress,
                                     XTApiAddresses.Default.FuturesSocketClientAddress,
                                     XTApiAddresses.Default.RestClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        /// <param name="name">Environment name</param>
        /// <param name="spotRestAddress">Spot REST address</param>
        /// <param name="usdtFuturesRestAddress">USDT futures REST address</param>
        /// <param name="coinFuturesRestAddress">Coin futures REST address</param>
        /// <param name="spotSocketStreamsAddress">Spot socket address</param>
        /// <param name="futuresSocketStreamsAddress">Futures socket address</param>
        /// <param name="restAddress">General REST address</param>
        public static XTEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string usdtFuturesRestAddress,
                        string coinFuturesRestAddress,
                        string spotSocketStreamsAddress,
                        string futuresSocketStreamsAddress,
                        string restAddress)
            => new XTEnvironment(name, spotRestAddress, usdtFuturesRestAddress, coinFuturesRestAddress, spotSocketStreamsAddress, futuresSocketStreamsAddress, restAddress);
    }
}
