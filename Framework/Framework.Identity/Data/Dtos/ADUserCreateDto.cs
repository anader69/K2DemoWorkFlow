using Framework.Core.DataAnnotations;
using Framework.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class ADUserCreateDto : UserCreateOrUpdateDtoBase
    {
        public bool ADUser { get; set; } = true;
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [DisableScripts]
        [StringLength(50, ErrorMessageResourceType = typeof(SharedResources),
            ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string UserName { get; set; }


        [DisableScripts]
        [StringLength(150, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "StringLengthErrorMessage")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddressMessage", ErrorMessageResourceType = typeof(SharedResources))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [DisableScripts]
        [StringLength(150, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "StringLengthErrorMessage")]
        public string FullName { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public List<Guid> UserRolesIds { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        public bool IsActive { get; set; }

        //[StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "NationalIdentityLengthError")]
        // [NumbersOnly]
        public string NationalId { get; set; }


        public string TelephoneNumber { get; set; }

        public virtual RoleViewModel UserRole { get; set; }


        public string JobTitle { get; set; }

        //kamel 04-09-2022
        public string SamAccountName { get; set; }
        public string SurName { get; set; }
        public string Description { get; set; }
        public string GivenName { get; set; }
        public string DisplayName { get; set; }
        public string Department { get; set; }  // not found
        public string Company { get; set; }  // not found
        public string Name { get; set; }
        public string UserPrincipalName { get; set; }
        public string IpPhone { get; set; }   // not found
        public string Mobile { get; set; }   // not found
        public string Manager { get; set; }   // not found
        public string ThumbnailPhoto { get; set; }   // not found 
        public string Info { get; set; }   // not found
        public string MailNickName { get; set; }   // not found
        public Guid? UserId { get; set; }




    }

    public class RoleViewModel
    {

        public string Code { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

    }


}