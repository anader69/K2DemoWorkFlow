// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Utils.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager
{
    /// <summary>
    /// The utils.
    /// </summary>
    internal static class Utils
    {
        /*security global*/

        /// <summary>
        /// The security label separator.
        /// </summary>
        public static string SecurityLabelSeparator => ":";

        /*types*/

        /// <summary>
        /// The type string to string.
        /// </summary>
        public static string TypeStringToString => "System.String";

        /*operations*/

        /// <summary>
        /// The rem fqn label.
        /// </summary>
        /// <param name="fqn">
        /// The fqn.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string RemFqnLabel(string fqn)
        {
            return fqn.Contains(":") ? fqn.Substring(fqn.IndexOf(':') + 1) : fqn;
        }
    }
}