// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Group.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.K2.SecurityManager
{
    #region usings

    using K2DemoWorkFlow.k2.SecurityManager;
    using SourceCode.Hosting.Server.Interfaces;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// The k 2 group.
    /// </summary>
    internal class K2Group : IGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="K2Group"/> class.
        /// </summary>
        /// <param name="securityLabel">
        /// The security label.
        /// </param>
        /// <param name="groupName">
        /// The group name.
        /// </param>
        /// <param name="groupDescription">
        /// The group description.
        /// </param>
        public K2Group(string securityLabel, string groupName, string groupDescription)
        {
            this.GroupName = groupName;
            this.GroupID = securityLabel + Utils.SecurityLabelSeparator + this.GroupName; /*fqn*/

            this.Properties = new Dictionary<string, object>();
            this.Properties.Add(K2Utils.K2GroupNamePropertyName, this.GroupID);
            this.Properties.Add(K2Utils.K2GroupDescriptionPropertyName, groupDescription);
        }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        public string GroupID { get; set; }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public IDictionary<string, object> Properties { get; set; }
    }
}