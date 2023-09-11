using Framework.Identity.Recources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{
    public class ChangePasswordAdminDto
    {
        [HiddenInput]
        public Guid UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "NewPassword", ResourceType = typeof(IdentityResources))]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ConfirmNewPassword", ResourceType = typeof(IdentityResources))]
        public string ConfirmPassword { get; set; }
    }

}
