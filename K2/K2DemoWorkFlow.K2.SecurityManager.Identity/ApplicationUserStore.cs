// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserStore.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using K2DemoWorkFlow.k2.SecurityManager.Identity.Entities;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity
{
    #region usings

    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    #endregion

    /// <summary>
    ///     The application user store.
    /// </summary>
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, Guid, UserLogin,
        UserRole, UserClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserStore"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public ApplicationUserStore(AppDbContext context)
            : base(context)
        {
        }
    }
}