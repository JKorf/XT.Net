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
        [Map("ONLINE")]
        Online,
        /// <summary>
        /// Offline
        /// </summary>
        [Map("OFFLINE")]
        Offline,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("DELISTED")]
        Delisted
    }
}
