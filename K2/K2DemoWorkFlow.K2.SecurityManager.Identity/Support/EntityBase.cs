using System;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Support
{
    /// <inheritdoc/>
    [Serializable]
    public abstract class EntityBase : IEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }

}
