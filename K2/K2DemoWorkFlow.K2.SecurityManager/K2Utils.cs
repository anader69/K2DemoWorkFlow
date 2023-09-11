// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Utils.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager
{
    #region usings

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The k 2 utils.
    /// </summary>
    internal static class K2Utils
    {
        /// <summary>
        /// The k 2 group description property name.
        /// </summary>
        public static string K2GroupDescriptionPropertyName => "Description";

        /*k2 group*/

        /// <summary>
        /// The k 2 group name property name.
        /// </summary>
        public static string K2GroupNamePropertyName => "Name";

        /// <summary>
        /// Gets the k 2 group property definitions.
        /// </summary>
        public static Dictionary<string, string> K2GroupPropertyDefinitions
        {
            get
            {
                var groupProperties = new Dictionary<string, string>
                                           {
                                               { K2GroupNamePropertyName, Utils.TypeStringToString },
                                               { K2GroupDescriptionPropertyName, Utils.TypeStringToString }
                                           };

                return groupProperties;
            }
        }

        /// <summary>
        /// The k 2 user common name property name.
        /// </summary>
        public static string K2UserCommonNamePropertyName => "CommonName";

        /// <summary>
        /// The k 2 user description property name.
        /// </summary>
        public static string K2UserDescriptionPropertyName => "Description";

        /// <summary>
        /// The k 2 user display name property name.
        /// </summary>
        public static string K2UserDisplayNamePropertyName => "DisplayName";

        /// <summary>
        /// The k 2 user email property name.
        /// </summary>
        public static string K2UserEmailPropertyName => "Email";

        /// <summary>
        /// The k 2 user manager property name.
        /// </summary>
        public static string K2UserManagerPropertyName => "Manager";

        /*k2 user*/

        /// <summary>
        /// The k 2 user name property name.
        /// </summary>
        public static string K2UserNamePropertyName => "Name";

        /// <summary>
        /// Gets the k 2 user property definitions.
        /// </summary>
        public static Dictionary<string, string> K2UserPropertyDefinitions
        {
            get
            {
                var userProperties = new Dictionary<string, string>
                                          {
                                              { K2UserNamePropertyName, Utils.TypeStringToString },
                                              { K2UserDescriptionPropertyName, Utils.TypeStringToString },
                                              { K2UserEmailPropertyName, Utils.TypeStringToString },
                                              { K2UserManagerPropertyName, Utils.TypeStringToString },
                                              { K2UserDisplayNamePropertyName, Utils.TypeStringToString },
                                              { K2UserCommonNamePropertyName, Utils.TypeStringToString }
                                          };

                return userProperties;
            }
        }
    }
}