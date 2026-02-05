using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Interfaces;
using CryptoExchange.Net.Trackers.UserData.Objects;

namespace XT.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IXTTrackerFactory : ITrackerFactory
    {
        /// <summary>
        /// Create a new Spot user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, ApiCredentials credentials, SpotUserDataTrackerConfig? config = null, XTEnvironment? environment = null);
        /// <summary>
        /// Create a new spot user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserSpotDataTracker CreateUserSpotDataTracker(SpotUserDataTrackerConfig? config = null);

        /// <summary>
        /// Create a new USDT futures user data tracker
        /// </summary>
        /// <param name="userIdentifier">User identifier</param>
        /// <param name="config">Configuration</param>
        /// <param name="credentials">Credentials</param>
        /// <param name="environment">Environment</param>
        IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, XTEnvironment? environment = null);
        /// <summary>
        /// Create a new USDT futures user data tracker
        /// </summary>
        /// <param name="config">Configuration</param>
        IUserFuturesDataTracker CreateUserUsdtFuturesDataTracker(FuturesUserDataTrackerConfig? config = null);

        ///// <summary>
        ///// Create a new Coin futures user data tracker
        ///// </summary>
        ///// <param name="userIdentifier">User identifier</param>
        ///// <param name="config">Configuration</param>
        ///// <param name="credentials">Credentials</param>
        ///// <param name="environment">Environment</param>
        //IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(string userIdentifier, ApiCredentials credentials, FuturesUserDataTrackerConfig? config = null, XTEnvironment? environment = null);
        ///// <summary>
        ///// Create a new Coin futures user data tracker
        ///// </summary>
        ///// <param name="config">Configuration</param>
        //IUserFuturesDataTracker CreateUserCoinFuturesDataTracker(FuturesUserDataTrackerConfig? config = null);
    }
}
