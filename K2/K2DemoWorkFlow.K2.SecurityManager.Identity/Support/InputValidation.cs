// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputValidation.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Support
{
    #region usings

    using System;

    #endregion

    /// <summary>
    ///     The input validation.
    /// </summary>
    public static class InputValidation
    {
        /// <summary>
        /// The argument is null.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="paramName">
        /// The param name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static void ArgumentIsNull(object obj, string paramName, string message = "")
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(paramName), message);
            }
        }

        /// <summary>
        /// The string is null or empty.
        /// </summary>
        /// <param name="str">
        /// The str.
        /// </param>
        /// <param name="paramName">
        /// The param name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static void StringArgumentIsNullOrEmpty(string str, string paramName, string message = "")
        {
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}