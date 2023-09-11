// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReturnResult.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Support
{
    #region usings

    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    ///     The error item.
    /// </summary>
    public class ErrorItem
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }

    /// <summary>
    /// The return result.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class ReturnResult<T>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult{T}" /> class.
        /// </summary>
        public ReturnResult()
        {
            this.Value = default(T);
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnResult{T}"/> class.
        /// </summary>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        public ReturnResult(T defaultValue)
        {
            this.Value = defaultValue;
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        ///     Gets or sets the model state.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        ///     The is valid.
        /// </summary>
        public bool IsValid => !this.Errors.Any();

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Gets or sets the return value.
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// The add error item.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddErrorItem(string name, string value)
        {
            this.Errors.Add(new ErrorItem { Name = name, Value = value });
        }
    }

    /// <summary>
    ///     The return result.
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnResult" /> class.
        ///     Initializes a new instance of the <see cref="ReturnResult{T}" /> class.
        /// </summary>
        public ReturnResult()
        {
            this.Errors = new List<ErrorItem>();
        }

        /// <summary>
        ///     Gets or sets the errors.
        /// </summary>
        public List<ErrorItem> Errors { get; set; }

        /// <summary>
        ///     The is valid.
        /// </summary>
        public bool IsValid => !this.Errors.Any();

        /// <summary>
        /// The add error item.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public void AddErrorItem(string name, string value)
        {
            this.Errors.Add(new ErrorItem { Name = name, Value = value });
        }
    }

    /// <summary>
    /// The return result extentsions.
    /// </summary>
    public static class ReturnResultExtentsions
    {
        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="result1">
        /// The result 1.
        /// </param>
        /// <param name="result2">
        /// The result 2.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Merge<T>(this ReturnResult<T> result1, ReturnResult result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="result1">
        /// The result 1.
        /// </param>
        /// <param name="result2">
        /// The result 2.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public static void Merge<T>(this ReturnResult result1, ReturnResult<T> result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="result1">
        /// The result 1.
        /// </param>
        /// <param name="result2">
        /// The result 2.
        /// </param>
        public static void Merge(this ReturnResult result1, ReturnResult result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="result1">
        /// The result 1.
        /// </param>
        /// <param name="result2">
        /// The result 2.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="T2">
        /// </typeparam>
        public static void Merge<T, T2>(this ReturnResult<T> result1, ReturnResult<T2> result2)
        {
            if (result2.IsValid)
            {
                return;
            }

            if (result2.Errors != null && result2.Errors.Any())
            {
                foreach (var item in result2.Errors)
                {
                    result1.AddErrorItem(item.Name, item.Value);
                }
            }
        }

        /// <summary>
        /// The merge.
        /// </summary>
        /// <param name="identityResult">
        /// The identity Result.
        /// </param>
        /// <param name="result">
        /// The result.
        /// </param>
        public static void Merge(this IdentityResult identityResult, ReturnResult result)
        {
            if (identityResult.Errors != null && !identityResult.Errors.Any())
            {
                return;
            }

            result = result ?? new ReturnResult();

            foreach (var item in identityResult.Errors)
            {
                result.AddErrorItem(string.Empty, item);
            }
        }

        /// <summary>
        /// The to string list.
        /// </summary>
        /// <param name="errors">
        /// The errors.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> ToStringList(this List<ErrorItem> errors)
        {
            return errors?.Select(e => e.Value).ToList() ?? new List<string>();
        }
    }
}