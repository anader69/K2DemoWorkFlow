using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity.Entities
{
    [Table("identity.UserTokens")]
    public partial class UserToken
    {
        [Key]
        [Column(Order = 0)]
        public Guid UserId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(64)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(64)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Value { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
