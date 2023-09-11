// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesSearchQuery.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Model
{
    /// <summary>
    /// The roles search query.
    /// </summary>
    public class RolesSearchQuery
    {
        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public int? Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }
    }
}