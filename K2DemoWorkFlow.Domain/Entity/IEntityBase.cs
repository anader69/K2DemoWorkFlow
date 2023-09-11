using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2DemoWorkFlow.Domain.Entity
{
    /// <summary>
    /// Defines an entity. It's primary key may not be "Id" or it may have a composite primary key.
    /// Use <see cref="IEntityBase{TKey}"/> where possible for better integration to repositories and other structures in the framework.
    /// </summary>
    public interface IEntityBase
    {

    }

    /// <summary>
    /// Defines an entity with a single primary key with "Id" property.
    /// </summary>
    /// <typeparam name="TKey">Type of the primary key of the entity</typeparam>
    public interface IEntityBase<TKey> : IEntityBase
    {
        /// <summary>
        /// Unique identifier for this entity.
        /// </summary>
        TKey Id { get; set; }
    }
}
