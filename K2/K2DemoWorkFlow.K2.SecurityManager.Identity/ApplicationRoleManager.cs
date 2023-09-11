// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationRoleManager.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using K2DemoWorkFlow.k2.SecurityManager.Identity.Entities;
using K2DemoWorkFlow.k2.SecurityManager.Identity.Model;
using K2DemoWorkFlow.k2.SecurityManager.Identity.Support;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity
{
    #region usings

    #endregion

    /// <summary>
    /// The application role manager.
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole, Guid>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Commons.UsersMgmt.ApplicationRoleManager" /> class.
        /// </summary>
        /// <param name="roleStore">The role store.</param>
        public ApplicationRoleManager(IRoleStore<ApplicationRole, Guid> roleStore)
            : base(roleStore)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationRoleManager"/> class.
        /// </summary>
        public ApplicationRoleManager()
            : this(new RoleStore<ApplicationRole, Guid, UserRole>(new AppDbContext()))
        {
        }

        /// <summary>
        /// Adds the specified role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="ReturnResult"/>.
        /// </returns>
        public ReturnResult AddRole(ApplicationRole role)
        {
            var result = new ReturnResult();

            // Check if role is null or not
            InputValidation.ArgumentIsNull(role, nameof(role));

            if (this.IsRoleExist(role.Name))
            {
                result.AddErrorItem(nameof(role.Name), "RoleAlreadyExsit");
            }

            if (this.IsRoleExist(role.Code))
            {
                result.AddErrorItem(nameof(role.Code), "RoleCodeAlreadyExsit");
            }

            if (this.IsRoleExistWithArabicName(role.DescriptionAr))
            {
                result.AddErrorItem(nameof(role.DescriptionAr), "RoleDescriptionArAlreadyExsit");
            }

            if (this.IsRoleExistWithArabicName(role.DescriptionEn))
            {
                result.AddErrorItem(nameof(role.DescriptionEn), "RoleDescriptionEnAlreadyExsit");
            }

            if (!result.IsValid)
            {
                return result;
            }

            var identityResult = this.Create(role);

            if (!identityResult.Succeeded)
            {
                identityResult.Merge(result);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Deletes the role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="ReturnResult"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ReturnResult DeleteRole(ApplicationRole role)
        {
            // Check if role is null or not
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var result = new ReturnResult();

            // Check if role is not exist
            if (!this.IsRoleExist(role.Id))
            {
                result.AddErrorItem(string.Empty, "RoleNotFound");
            }

            if (!result.IsValid)
            {
                return result;
            }

            var identityResult = this.Delete(role);

            if (!identityResult.Succeeded)
            {
                identityResult.Merge(result);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Deletes the role by identifier.
        /// </summary>
        /// <param name="roleId">
        /// The role identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ReturnResult"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ReturnResult DeleteRole(Guid roleId)
        {
            var result = new ReturnResult();

            // Check if role is null or not
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            // Check if role is not exist
            if (!this.IsRoleExist(roleId))
            {
                result.AddErrorItem(string.Empty, "RoleNotFound");
            }

            if (!result.IsValid)
            {
                return result;
            }

            var applicationRole = this.FindById(roleId);

            if (applicationRole == null)
            {
                result.AddErrorItem(string.Empty, "RoleNotFound");
                return result;
            }

            var identityResult = this.Delete(applicationRole);

            if (!identityResult.Succeeded)
            {
                identityResult.Merge(result);
                return result;
            }

            return result;
        }

        /// <summary>
        /// Finds the users in role.
        /// </summary>
        /// <param name="roleId">
        /// The role identifier.
        /// </param>
        /// <returns>
        /// The <see cref="ICollection"/>.
        /// </returns>
        public ICollection<UserRole> FindUsersInRole(Guid roleId)
        {
            // Check if role is null or not
            if (roleId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            // Check if role is not exist
            if (!this.IsRoleExist(roleId))
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            return this.FindById(roleId).Users;
        }

        /// <summary>
        /// The get all roles.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ApplicationRole> GetAllRoles()
        {
            using (var dbContext = new AppDbContext())
            {
                var items = dbContext.AspNetRoles.ToList();
                return items;
            }
        }

        /// <summary>
        /// The get group name.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetGroupName(int code)
        {
            var role = this.Roles.FirstOrDefault(r => r.Code == code);
            return role?.Name;
        }

        /// <summary>
        /// The get group name ar.
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetGroupNameAr(int code)
        {
            var role = this.Roles.FirstOrDefault(r => r.Code == code);
            return role?.DescriptionAr;
        }

        /// <summary>
        /// Determines whether [is role exist] [the specified identifier].
        /// </summary>
        /// <param name="id">
        /// The identifier.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRoleExist(Guid id)
        {
            // Check if role is null or not
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return this.Roles.Any(a => a.Id == id);
        }

        /// <summary>
        /// Determines whether [is role exist] [the specified role name].
        /// </summary>
        /// <param name="roleName">
        /// Name of the role.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRoleExist(string roleName)
        {
            return this.Roles.Any(a => a.Name == roleName);
        }

        /// <summary>
        /// Determines whether [is role exist] [the specified code].
        /// </summary>
        /// <param name="code">
        /// The code.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRoleExist(int code)
        {
            return this.Roles.Any(a => a.Code == code);
        }

        /// <summary>
        /// The is role exist with arabic name.
        /// </summary>
        /// <param name="roleNameAr">
        /// The role name ar.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRoleExistWithArabicName(string roleNameAr)
        {
            return this.Roles.Any(a => a.DescriptionAr == roleNameAr);
        }

        /// <summary>
        /// Searches the roles.
        /// </summary>
        /// <param name="sq">
        /// The sq.
        /// </param>
        /// <returns>
        /// The <see cref="PagedList"/>.
        /// </returns>
        public PagedList<ApplicationRole> SearchRoles(RolesSearchQuery sq)
        {
            var query = this.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(sq.Name))
            {
                query = query.Where(a => a.Name.Contains(sq.Name));
            }

            if (!string.IsNullOrEmpty(sq.DisplayName))
            {
                query = query.Where(
                    a => a.DisplayNameAr.Contains(sq.DisplayName) || a.DisplayNameEn.Contains(sq.DisplayName));
            }

            if (!string.IsNullOrEmpty(sq.Description))
            {
                query = query.Where(
                    a => a.DescriptionEn.Contains(sq.Description) || a.DescriptionAr.Contains(sq.Description));
            }

            return query.GetPaged(x => x.Code, false, sq.PageNumber, sq.PageSize);
        }

        /// <summary>
        /// Updates the role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="ReturnResult"/>.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// </exception>
        public ReturnResult UpdateRole(ApplicationRole role)
        {
            var result = new ReturnResult();

            // Check if role is null or not
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            // Check if role is not exist
            if (!this.IsRoleExist(role.Id))
            {
                result.AddErrorItem(string.Empty, "RoleNotFound");
            }

            if (!result.IsValid)
            {
                return result;
            }

            var identityResult = this.Update(role);

            if (!identityResult.Succeeded)
            {
                identityResult.Merge(result);
                return result;
            }

            return result;
        }
    }
}