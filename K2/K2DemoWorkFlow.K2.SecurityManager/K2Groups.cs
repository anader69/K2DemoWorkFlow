// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Groups.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace K2DemoWorkFlow.k2.SecurityManager
{
    #region usings

    using SourceCode.Hosting.Server.Interfaces;
    using System.Collections;
    using System.Collections.ObjectModel;

    #endregion

    /// <summary>
    /// The k 2 groups.
    /// </summary>
    internal class K2Groups : IGroupCollection
    {
        /// <summary>
        /// The _collection.
        /// </summary>
        private readonly Collection<IGroup> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Groups"/> class.
        /// </summary>
        public K2Groups()
        {
            this.collection = new Collection<IGroup>();
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="IGroup"/>.
        /// </returns>
        public IGroup this[int index] => this.collection[index];

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="group">
        /// The group.
        /// </param>
        public void Add(IGroup group)
        {
            this.collection.Add(group);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.collection.GetEnumerator();
        }
    }
}