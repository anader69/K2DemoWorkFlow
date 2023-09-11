using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.UserClaims")]
    public partial class UserClaim : IdentityUserClaim<Guid>
    {
        [Required]
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }

        public virtual ApplicationUser AspNetUser { get; set; }
    }
}
