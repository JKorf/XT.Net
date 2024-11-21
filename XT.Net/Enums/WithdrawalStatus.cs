using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Withdrawal status
    /// </summary>
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
