// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2User.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager
{
    #region usings

    using SourceCode.Hosting.Server.Interfaces;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The k 2 user.
    /// </summary>
    internal class K2User : IUser
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="K2User"/> class.
        /// </summary>
        /// <param name="securityLabel">
        /// The security label.
        /// </param>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <param name="userDescription">
        /// The user description.
        /// </param>
        /// <param name="userEmail">
        /// The user email.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        public K2User(
            string securityLabel,
            string userName,
            string userDescription,
            string userEmail,
            string userManager)
        {
            this.UserName = userName;
            this.UserID = securityLabel + Utils.SecurityLabelSeparator + userName; /*fqn*/

            this.Properties = new Dictionary<string, object>
                                  {
                                      { K2Utils.K2UserNamePropertyName, this.UserName },
                                      { K2Utils.K2UserDescriptionPropertyName, userDescription },
                                      { K2Utils.K2UserEmailPropertyName, userEmail },
                                      { K2Utils.K2UserManagerPropertyName, userManager }
                                  };
        }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }
    }
}