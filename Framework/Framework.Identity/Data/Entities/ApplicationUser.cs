using Framework.Core;
using Framework.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.Identity.Data.Entities
{
    //test
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser(string userName, string fullName, string email = null, bool isActive = true)
        {
            Check.NotNull(userName, nameof(userName));

            Id = Guid.NewGuid().AsSequentialGuid();
            UserName = userName.ToLower();
            FullName = fullName;
            NormalizedUserName = userName.ToUpperInvariant();
            Email = email?.ToLower();
            NormalizedEmail = email?.ToUpperInvariant();
            SecurityStamp = Guid.NewGuid().ToString();
            IsActive = isActive;
        }
        public string FullName { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string JobTitle { get; set; }
    }
}
