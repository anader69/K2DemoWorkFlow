using Framework.Core.Data;
using Framework.Core.DataAnnotations;
using Framework.Identity.Data.Entities;
using Framework.Identity.Recources;
using Framework.Resources;
using PagedList.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class IdentityUserSearchDto : PagingDto
    {
        [Display(Name = "FullName", ResourceType = typeof(IdentityResources))]
        public string FullName { get; set; }
        [Display(Name = "Email", ResourceType = typeof(IdentityResources))]
        public string Email { get; set; }
        [Display(Name = "PhoneNumber", ResourceType = typeof(IdentityResources))]
        [ValidatePhoneNumber(ErrorMessageResourceName = "InvalidMobileNumber", ErrorMessageResourceType = typeof(IdentityResources))]
        public string PhoneNumber { get; set; }
        [Display(Name = "ActivationStatus", ResourceType = typeof(SharedResources))]
        public bool? IsActive { get; set; }

        [Display(Name = "IdentityNumber", ResourceType = typeof(IdentityResources))]
        public string IdentityNo { get; set; }


        public bool IsExternalUser { get; set; } = false;

        public new StaticPagedList<ApplicationUser> Items { get; set; }
        public List<ApplicationUser> ExportedItems { get; set; }


    }
    public class UserGridSearchDto : PagingDto
    {
        public string UserTxtSearch { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }
        public int TotalItemsCount { get; set; }
        public new StaticPagedList<ApplicationUser> Items { get; set; }
        public List<ApplicationUser> ExportedItems { get; set; }
    }

}