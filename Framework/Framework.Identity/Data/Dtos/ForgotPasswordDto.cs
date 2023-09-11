using Framework.Core.DataAnnotations;
using Framework.Core.Notifications;
using Framework.Identity.Recources;
using Framework.Resources;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class ForgotPasswordDto
    {
        [Display(Name = "Email", ResourceType = typeof(IdentityResources))]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$", ErrorMessage = null, ErrorMessageResourceType = typeof(IdentityResources), ErrorMessageResourceName = "EmailValidator")]
        [Required(ErrorMessage = null, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Email { get; set; }

        [ValidatePhoneNumber(ErrorMessageResourceName = "InvalidMobileNumber", ErrorMessageResourceType = typeof(IdentityResources))]
        [Display(Name = "PhoneNumber", ResourceType = typeof(IdentityResources))]
        public string PhoneNumber { get; set; }
        public NotificationTypes NotificationTypes { get; set; }

    }
}
