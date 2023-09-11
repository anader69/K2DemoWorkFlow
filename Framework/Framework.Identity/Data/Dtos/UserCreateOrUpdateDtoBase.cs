using Framework.Core.DataAnnotations;
using Framework.Identity.Recources;
using Framework.Resources;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public abstract class UserCreateOrUpdateDtoBase
    {
        [StringLength(256)]
        [Display(Name = "UserName", ResourceType = typeof(IdentityResources))]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string UserName { get; set; }

        // [StringLength(UserConsts.MaxNameLength)]
        [StringLength(256)]
        [Display(Name = "FullName", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [DisableScripts]
        public string FullName { get; set; }

        [StringLength(256)]
        [Display(Name = "JobTitle", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [DisableScripts]
        public string JobTitle { get; set; }

        [StringLength(256)]
        [Display(Name = "Department", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [DisableScripts]
        public string Department { get; set; }


        [EmailAddress(ErrorMessageResourceName = "EmailValidator", ErrorMessageResourceType = typeof(IdentityResources))]
        [StringLength(256)]
        [Display(Name = "Email", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]

        public string Email { get; set; }


        [StringLength(256)]
        [Display(Name = "PhoneNumber", ResourceType = typeof(IdentityResources))]
        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        //[ValidatePhoneNumber(ErrorMessageResourceName = "InvalidMobileNumber", ErrorMessageResourceType = typeof(IdentityResources))]
        //[MaxLength(14, ErrorMessage = null, ErrorMessageResourceName = "PhoneNumberLimitation", ErrorMessageResourceType = typeof(SharedResources)), MinLength(11, ErrorMessage = null, ErrorMessageResourceName = "PhoneNumberLimitation", ErrorMessageResourceType = typeof(SharedResources))]
        public string PhoneNumber { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public bool LockoutEnabled { get; set; }

        public bool IsActive { get; set; }

        public string[] RoleNames { get; set; }
    }
}