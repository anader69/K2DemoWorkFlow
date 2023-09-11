using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    /// <summary>
    /// Represents the link between a user and a role.
    /// </summary>
    [Table("identity.UserRoles")]
    public class UserRole : IdentityUserRole<Guid>
    {
        [Key]
        [ForeignKey(nameof(User))]
        [Column(Order = 0)]
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }

        [Key]
        [ForeignKey(nameof(Role))]
        [Column(Order = 1)]
        public override Guid RoleId { get => base.RoleId; set => base.RoleId = value; }

        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
    }
}