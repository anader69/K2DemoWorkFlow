using K2DemoWorkFlow.k2.SecurityManager.Identity.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.Roles")]
    public partial class ApplicationRole : IdentityRole<Guid, UserRole>
    {
        public ApplicationRole()
        {
            AspNetUsers = new HashSet<ApplicationUser>();
        }

        public virtual ICollection<ApplicationUser> AspNetUsers { get; set; }

        public string NormalizedName { get; internal set; }
        public string ConcurrencyStamp { get; internal set; }
        public string RoleGroup { get; internal set; }
        public bool IsDefault { get; internal set; }
        //public Guid? ReportingToRoleId { get; internal set; }


        public int Code { get; internal set; }
        public string DescriptionAr { get; internal set; }
        public string DescriptionEn { get; internal set; }
        public string DisplayNameAr { get; internal set; }
        public string DisplayNameEn { get; internal set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedOn { get; set; }
    }
}
