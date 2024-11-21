using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// XT options
    /// </summary>
    public class XTOptions
    {
        /// <summary>
        /// Rest client options
        /// </summary>
        public XTRestOptions Rest { get; set; } = new XTRestOptions();

        /// <summary>
        /// Socket client options
        /// </summary>
        public XTSocketOptions Socket { get; set; } = new XTSocketOptions();

        /// <summary>
        /// Trade environment. Contains info about URL's to use to connect to the API. Use `XTEnvironment` to swap environment, for example `Environment = XTEnvironment.Live`
        /// </summary>
        public XTEnvironment? Environment { get; set; }

        /// <summary>
        /// The api credentials used for signing requests.
        /// </summary>
        public ApiCredentials? ApiCredentials { get; set; }

        /// <summary>
        /// The DI service lifetime for the IXTSocketClient
        /// </summary>
        public ServiceLifetime? SocketClientLifeTime { get; set; }
    }
}
