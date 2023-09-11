using Framework.Core;
using Framework.Core.DataAnnotations;
using Framework.Identity.Data.Entities;
using Framework.Resources;
using PagedList.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace Framework.Identity.Data.Dtos
{

    /// <summary>
    ///     The users list view model.
    /// </summary>
    public class UsersListViewModel
    {
        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        [DisableScripts]
        [StringLength(
            150,
            ErrorMessageResourceType = typeof(SharedResources),
            ErrorMessageResourceName = "RequiredFieldMessage")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(
            ErrorMessageResourceName = "InvalidEmailAddressMessage",
            ErrorMessageResourceType = typeof(SharedResources))]
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        [DisableScripts]
        [StringLength(
            150,
            ErrorMessageResourceType = typeof(SharedResources),
            ErrorMessageResourceName = "RequiredFieldMessage")]
        public string FullName { get; set; }


        /// <summary>
        ///     Gets or sets a value indicating whether is active.
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        public StaticPagedList<ApplicationUser> Items { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the page size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets the user name.
        /// </summary>
        [DisableScripts]
        [StringLength(
            50,
            ErrorMessageResourceType = typeof(SharedResources),
            ErrorMessageResourceName = "RequiredFieldMessage")]
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the user role id.
        /// </summary>
        public int? UserRoleId { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "RequiredFieldMessage")]
        [NumbersOnly]
        public String NationalId { get; set; }

        [DisableScripts]
        [StringLength(50, ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "StringLengthErrorMessage")]
        [DataType(DataType.PhoneNumber)]
        [ValidatePhoneNumber(ErrorMessageResourceName = "InvalidPhoneNumberMessage", ErrorMessageResourceType = typeof(SharedResources), NumberType = NumberType.FIXED_LINE_OR_MOBILE, CountryCode = "SA")]
        public String Phone { get; set; }

    }

}
