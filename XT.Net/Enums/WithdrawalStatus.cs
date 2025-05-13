using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Attributes;

namespace XT.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<WithdrawalStatus>))]
    public enum WithdrawalStatus
    {
        /// <summary>
        /// Submited
        /// </summary>
        [Map("SUBMIT")]
        Submited,
        /// <summary>
        /// In review
        /// </summary>
        [Map("REVIEW")]
        InReview,
        /// <summary>
        /// Audited
        /// </summary>
        [Map("AUDITED")]
        Audited,
        /// <summary>
        /// Audited again
        /// </summary>
        [Map("AUDITED_AGAIN")]
        AuditedAgain,
        /// <summary>
        /// Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// Fail
        /// </summary>
        [Map("FAIL")]
        Fail,
        /// <summary>
        /// Canceled
        /// </summary>
        [Map("CANCEL")]
        Canceled,
    }

}
