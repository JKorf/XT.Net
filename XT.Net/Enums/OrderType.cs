using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Order type
    /// </summary>
    public enum OrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("LIMIT")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("MARKET")]
        Market
    }
}
