using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.Users")]
    public partial class ApplicationUser : IdentityUser<Guid, UserLogin, UserRole, UserClaim>
    {
        public ApplicationUser()
        {
            AspNetUserClaims = new HashSet<UserClaim>();
            AspNetUserLogins = new HashSet<UserLogin>();
            AspNetRoles = new HashSet<ApplicationRole>();
        }

        public virtual ICollection<UserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<UserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<ApplicationRole> AspNetRoles { get; set; }
        public string NormalizedEmail { get; internal set; }
        public string NormalizedUserName { get; internal set; }
        public string FullName { get; internal set; }
        public bool IsActive { get; internal set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }

        [StringLength(100)]
        public string UpdatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdatedOn { get; set; }

        [NotMapped]
        public new DateTime? LockoutEndDateUtc { get; set; }

    }
}
