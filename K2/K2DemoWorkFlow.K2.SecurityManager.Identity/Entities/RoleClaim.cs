using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.RoleClaims")]
    public partial class RoleClaim
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string ClaimType { get; set; }

        [StringLength(1024)]
        public string ClaimValue { get; set; }

        public Guid RoleId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}
