namespace XT.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class XTApiAddresses
    {
        /// <summary>
        /// The address used by the XTRestClient for the API
        /// </summary>
        public string SpotRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the XTSocketClient for the websocket API
        /// </summary>
        public string SocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the XT API
        /// </summary>
        public static XTApiAddresses Default = new XTApiAddresses
        {
            SpotRestClientAddress = "https://sapi.xt.com",
            SocketClientAddress = "wss://stream.xt.com"
        };
    }
}
