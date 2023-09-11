using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.UserLogins")]
    public partial class UserLogin : IdentityUserLogin<Guid>
    {
        [Key]
        [Column(Order = 0)]
        public override string LoginProvider { get => base.LoginProvider; set => base.LoginProvider = value; }

        [Key]
        [Column(Order = 1)]
        public override string ProviderKey { get => base.ProviderKey; set => base.ProviderKey = value; }

        [Key]
        [Column(Order = 2)]
        public override Guid UserId { get => base.UserId; set => base.UserId = value; }

        public virtual ApplicationUser AspNetUser { get; set; }
    }
}
