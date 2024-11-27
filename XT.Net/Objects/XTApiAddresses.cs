namespace XT.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class XTApiAddresses
    {
        /// <summary>
        /// The address used by the XTRestClient for the Spot API
        /// </summary>
        public string SpotRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the XTRestClient for the Coin futures API
        /// </summary>
        public string CoinFuturesRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the XTRestClient for the USDT futures API
        /// </summary>
        public string UsdtFuturesRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the XTSocketClient for the websocket spot API
        /// </summary>
        public string SpotSocketClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the XTSocketClient for the websocket futures API
        /// </summary>
        public string FuturesSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the XT API
        /// </summary>
        public static XTApiAddresses Default = new XTApiAddresses
        {
            SpotRestClientAddress = "https://sapi.xt.com",
            CoinFuturesRestClientAddress = "https://dapi.xt.com",
            UsdtFuturesRestClientAddress = "https://fapi.xt.com",
            SpotSocketClientAddress = "wss://stream.xt.com",
            FuturesSocketClientAddress = "wss://fstream.xt.com"
        };
    }
}
