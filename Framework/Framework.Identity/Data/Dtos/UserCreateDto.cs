using Framework.Identity.Recources;
using Framework.Resources;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class UserCreateDto : UserCreateOrUpdateDtoBase
    {
        //TODO : Un Comment Required to use password but in my case I Generate password

        //  [StringLength(UserConsts.MaxPasswordLength)]
        [StringLength(2556)]
        [Display(Name = "Password", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Password { get; set; }

        [StringLength(256)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string PasswordConfirmation { get; set; }

        public bool IsExternalUser { get; set; }

        [EmailAddress(ErrorMessageResourceName = "EmailValidator", ErrorMessageResourceType = typeof(IdentityResources))]
        [StringLength(256)]
        [Display(Name = "EmailConfirm", ResourceType = typeof(IdentityResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [Compare(nameof(Email), ErrorMessageResourceType = typeof(IdentityResources), ErrorMessageResourceName = "EmailConfirmNotMatchValidator")]
        public string EmailConfirmation { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }


    }
}