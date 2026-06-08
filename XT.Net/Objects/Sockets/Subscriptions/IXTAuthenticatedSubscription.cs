namespace XT.Net.Objects.Sockets.Subscriptions
{
    /// <summary>
    /// Marker for XT websocket subscriptions that carry a listen key / token in the subscribe payload.
    /// </summary>
    internal interface IXTAuthenticatedSubscription
    {
        /// <summary>
        /// Listen key / websocket token sent with the subscribe and unsubscribe requests.
        /// </summary>
        string? Token { get; set; }
    }
}
