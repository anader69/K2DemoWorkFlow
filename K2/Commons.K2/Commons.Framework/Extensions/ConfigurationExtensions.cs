// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System.Collections.Specialized;

    #endregion

    /// <summary>
    /// The configuration extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="coll">
        /// The coll.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="defaultVal">
        /// The default val.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T GetValue<T>(this NameValueCollection coll, string key, T defaultVal = default(T))
        {
            return coll[key].To(defaultVal);
        }
    }
}