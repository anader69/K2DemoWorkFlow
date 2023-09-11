// --------------------------------------------------------------------------------------------------------------------
// <copyright file="K2Users.cs" company="SURE International Technology">
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
    /// The k 2 users.
    /// </summary>
    internal class K2Users : IUserCollection
    {
        /// <summary>
        /// The _collection.
        /// </summary>
        private readonly Collection<IUser> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="K2Users"/> class.
        /// </summary>
        public K2Users()
        {
            this.collection = new Collection<IUser>();
        }

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="IUser"/>.
        /// </returns>
        public IUser this[int index] => this.collection[index];

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        public void Add(IUser user)
        {
            this.collection.Add(user);
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