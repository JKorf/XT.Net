using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Enums
{
    /// <summary>
    /// Deposit status
    /// </summary>
    public enum DepositStatus
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
        Review,
        /// <summary>
        /// Audited
        /// </summary>
        [Map("AUDITED")]
        Audited,
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
