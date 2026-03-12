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
        /// ["<c>SUBMIT</c>"] Submited
        /// </summary>
        [Map("SUBMIT")]
        Submited,
        /// <summary>
        /// ["<c>REVIEW</c>"] In review
        /// </summary>
        [Map("REVIEW")]
        InReview,
        /// <summary>
        /// ["<c>AUDITED</c>"] Audited
        /// </summary>
        [Map("AUDITED")]
        Audited,
        /// <summary>
        /// ["<c>AUDITED_AGAIN</c>"] Audited again
        /// </summary>
        [Map("AUDITED_AGAIN")]
        AuditedAgain,
        /// <summary>
        /// ["<c>PENDING</c>"] Pending
        /// </summary>
        [Map("PENDING")]
        Pending,
        /// <summary>
        /// ["<c>SUCCESS</c>"] Success
        /// </summary>
        [Map("SUCCESS")]
        Success,
        /// <summary>
        /// ["<c>FAIL</c>"] Fail
        /// </summary>
        [Map("FAIL")]
        Fail,
        /// <summary>
        /// ["<c>CANCEL</c>"] Canceled
        /// </summary>
        [Map("CANCEL")]
        Canceled,
    }

}
