using System;

namespace Framework.Core.Data
{
    [Serializable]
    public abstract class EntityBase : IEntityBase
    {

    }

    [Serializable]
    public abstract class EntityBase<TKey> : EntityBase, IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }

    [Serializable]
    public abstract class FullAuditedEntityBase : EntityBase
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

    }


    [Serializable]
    public abstract class FullAuditedEntityBase<TKey> : EntityBase<TKey>
    {
        //[Column(Order = 300)]
        public string CreatedBy { get; set; }

        //[Column(Order = 301)]
        public DateTime CreatedOn { get; set; }

        //[Column(Order = 302)]
        public string UpdatedBy { get; set; }

        //[Column(Order = 303)]
        public DateTime? UpdatedOn { get; set; }

    }

    [Serializable]
    public class LookupEntityBase : FullAuditedEntityBase
    {
        public int Id { get; set; }
        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public bool IsActive { get; set; }
    }


    [Serializable]
    public abstract class LookupEntityBase<TKey> : FullAuditedEntityBase<TKey>
    {
        public string NameAr { get; set; }

        public string NameEn { get; set; }

        public bool IsActive { get; set; }
    }


}
