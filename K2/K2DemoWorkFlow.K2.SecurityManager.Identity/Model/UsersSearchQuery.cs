// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersSearchQuery.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Model
{
    #region usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     The users search query.
    /// </summary>
    public class UsersSearchQuery
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UsersSearchQuery" /> class.
        /// </summary>
        public UsersSearchQuery()
        {
            this.RolesCodes = new List<int>();
            this.RolesNames = new List<string>();
        }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the first name.
        /// </summary>
        /// <value>
        ///     The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        /// <value>
        ///     The full name.
        /// </value>
        public string FullName { get; set; }

        /// <summary>
        ///     Gets or sets the is activated.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the last name.
        /// </summary>
        /// <value>
        ///     The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        ///     The name of the middle.
        /// </value>
        public string MiddleName { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets the identity Number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the role.
        /// </summary>
        public List<int> RolesCodes { get; set; }

        /// <summary>
        /// Gets or sets the roles names.
        /// </summary>
        public List<string> RolesNames { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}