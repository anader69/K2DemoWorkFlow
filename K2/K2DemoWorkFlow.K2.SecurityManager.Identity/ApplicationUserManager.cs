// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationUserManager.cs" company="SURE International Technology">
//   Copyright © 2018 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using K2DemoWorkFlow.k2.SecurityManager.Identity.Entities;

namespace K2DemoWorkFlow.k2.SecurityManager.Identity
{
    #region usings

    using K2DemoWorkFlow.k2.SecurityManager.Identity.Model;
    using K2DemoWorkFlow.k2.SecurityManager.Identity.Support;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// The application user manager.
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser, Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        /// <param name="store">
        /// The store.
        /// </param>
        public ApplicationUserManager(IUserStore<ApplicationUser, Guid> store)
            : base(store)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserManager"/> class.
        /// </summary>
        public ApplicationUserManager()
            : this(new ApplicationUserStore(new AppDbContext()))
        {
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task Add(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task Delete(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The find users by user name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ApplicationUser> FindUsersByUserName(string name)
        {
            using (var dbContext = new AppDbContext())
            {
                var items = dbContext.AspNetUsers.Where(x => x.UserName.Contains(name)).ToList();
                return items;
            }
        }

        /// <summary>
        /// The get all users.
        /// </summary>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<ApplicationUser> GetAllUsers()
        {
            using (var dbContext = new AppDbContext())
            {
                var items = dbContext.AspNetUsers.ToList();
                return items;
            }
        }

        /// <summary>
        /// The get by email.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationUser"/>.
        /// </returns>
        public ApplicationUser GetByEmail(string email)
        {
            using (var dbContext = new AppDbContext())
            {
                var user = dbContext.AspNetUsers.FirstOrDefault(x => x.NormalizedEmail.Equals(email.ToUpper()));
                return user;
            }
        }

        /// <summary>
        /// The get by employee number async.
        /// </summary>
        /// <param name="employeeNumber">
        /// The employee number.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task<ApplicationUser> GetByEmployeeNumberAsync(string employeeNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationUser"/>.
        /// </returns>
        public ApplicationUser GetById(Guid userId)
        {
            using (var dbContext = new AppDbContext())
            {
                var user = dbContext.AspNetUsers.FirstOrDefault(x => x.Id == userId);
                return user;
            }
        }

        /// <summary>
        /// The get by login provider.
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task<ApplicationUser> GetByLoginProvider(string provider, string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get by user name.
        /// </summary>
        /// <param name="userName">
        /// The user name.
        /// </param>
        /// <returns>
        /// The <see cref="ApplicationUser"/>.
        /// </returns>
        public ApplicationUser GetByUserName(string userName)
        {
            using (var dbContext = new AppDbContext())
            {
                var user = dbContext.AspNetUsers.FirstOrDefault(x => x.NormalizedUserName.ToUpper().Equals(userName.ToUpper()));
                return user;
            }
        }

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The search.
        /// </summary>
        /// <param name="sq">
        /// The sq.
        /// </param>
        /// <returns>
        /// The <see cref="PagedList"/>.
        /// </returns>
        public PagedList<ApplicationUser> Search(UsersSearchQuery sq)
        {
            using (var dbContext = new AppDbContext())
            {
                var query = this.Users.Include(c => c.Roles).AsQueryable();

                if (!string.IsNullOrEmpty(sq.UserName))
                {
                    query = query.Where(a => a.UserName.Contains(sq.UserName));
                }

                if (!string.IsNullOrEmpty(sq.FullName))
                {
                    query = query.Where(a => a.FullName.Contains(sq.FullName));
                }

                //if (!string.IsNullOrEmpty(sq.MiddleName))
                //{
                //    query = query.Where(a => a.MiddleName.Contains(sq.MiddleName));
                //}

                //if (!string.IsNullOrEmpty(sq.LastName))
                //{
                //    query = query.Where(a => a.LastName.Contains(sq.LastName));
                //}

                if (!string.IsNullOrEmpty(sq.Email))
                {
                    query = query.Where(a => a.Email.Equals(sq.Email));
                }

                if (!string.IsNullOrEmpty(sq.PhoneNumber))
                {
                    query = query.Where(a => a.PhoneNumber.Equals(sq.PhoneNumber));
                }

                if (sq.IsActive.HasValue)
                {
                    query = query.Where(a => a.IsActive == sq.IsActive);
                }

                if (sq.RolesCodes != null && sq.RolesCodes.Any())
                {
                    var allRoles = dbContext.AspNetRoles.ToList();
                    var roleIds = sq.RolesCodes.Select(i => allRoles.FirstOrDefault(x => x.Code == i)?.Id);
                    if (roleIds.Any())
                    {
                        query = query.Where(u => u.Roles.Any(ur => roleIds.Contains(ur.RoleId)));
                    }
                }

                if (sq.RolesNames != null && sq.RolesNames.Any())
                {
                    ///FIX:: K2 Group Resolve Issue
                    //var allRoles = dbContext.AspNetRoles.ToList();
                    //var roleIds = sq.RolesNames.Select(i => allRoles.FirstOrDefault(x => x.Name == i)?.Id);

                    var roleIds = (from role in dbContext.AspNetRoles
                                   where sq.RolesNames.Contains(role.Name.ToLower())
                                   select role.Id).ToList();
                    if (roleIds.Any())
                    {
                        query = query.Where(u => u.Roles.Any(ur => roleIds.Contains(ur.RoleId)));
                    }
                }

                return query.GetPaged(x => x.UserName, false, sq.PageNumber, 5000);
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public Task Update(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The update user with roles async.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="roles">
        /// The roles.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<ReturnResult> UpdateUserWithRolesAsync(ApplicationUser user, string[] roles)
        {
            InputValidation.ArgumentIsNull(user, nameof(user));
            if (roles == null)
                roles = new string[0];

            var result = new ReturnResult();

            IdentityResult identityResult;
            if (roles != null && roles.Any())
            {
                var userRoles = this.GetRoles(user.Id);
                var selectedRole = roles.Select(r => r).ToArray();

                var rolestoRemoveFrom = userRoles.Except(selectedRole).ToArray();
                if (rolestoRemoveFrom.Any())
                {
                    identityResult = this.RemoveFromRoles(user.Id, rolestoRemoveFrom);

                    if (!identityResult.Succeeded)
                    {
                        identityResult.Merge(result);
                        return result;
                    }
                }

                var rolesToAddTo = selectedRole.Except(userRoles).ToArray();
                if (rolesToAddTo.Any())
                {
                    identityResult = this.AddToRoles(user.Id, rolesToAddTo);

                    if (!identityResult.Succeeded)
                    {
                        identityResult.Merge(result);
                        return result;
                    }
                }
            }

            identityResult = await this.UpdateAsync(user);
            if (!identityResult.Succeeded)
            {
                identityResult.Merge(result);
                return result;
            }

            return result;
        }
    }
}