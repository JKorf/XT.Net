using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Symbol status
    /// </summary>
    public enum SymbolStatus
    {
        /// <summary>
        /// Online
        /// </summary>
        [Map("ONLINE", "0")]
        Online,
        /// <summary>
        /// Offline
        /// </summary>
        [Map("OFFLINE", "1")]
        Offline,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("DELISTED")]
        Delisted
    }
}
