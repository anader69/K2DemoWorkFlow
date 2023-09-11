using Framework.Core;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Identity.Data.Services.Interfaces
{
    public interface IUserAppService
    {
        public string CurrentUserName { get; }
        public string CurrentUserEmail { get; }
        public Task<UserDto> GetAsync(Guid id);

        public Task<ApplicationUser> GetUserAsync(Guid id);
        public Task<UserDto> GetUserInfoAsync(Guid id);
        public Task<UserGridSearchDto> GetGridList(UserGridSearchDto model);
        public Task<bool> ChangeStatus(Guid id);

        //public IdentityUserSearchDto GetList(IdentityUserSearchDto model);

        public Task<List<string>> FindUsersInRoleAsync(string roleName);
        public Task<bool> ChangePassword(Guid userId, string password);

        public Task<UserDto> FindByIdAsync(Guid? id);

        public Task<List<RoleDto>> GetRolesAsync(Guid id);
        public Task<IList<string>> GetRolesAsync(ApplicationUser user);

        public List<SelectListItem> GetRoles();

        public string GetCurrentUserRolesDisplayName();
        public Task<string> GetRoleDisplayName(string roleName);

        Task<List<SelectListItem>> GetAvailableUserRoles(Guid id);

        public Task<List<string>> GetRolesDisplayNameAsync(Guid id);

        public Task<UserDto> UpdateAsync(Guid id, UserUpdateDto input);
        public Task<Guid> UpdateAsync(UserDto input);
        public Task<UserManagementInsertResultDto> IsUserExistAsync(string userName);

        public Task UpdateBasicInfoAsync(UserDto input);

        public Task<bool> DeleteAsync(Guid id);

        public Task AddRolesAsync(Guid id, string[] roleNames);
        public Task AddRoleAsync(Guid id, string roleName);

        public Task<UserDto> FindByUsernameAsync(string username);

        public Task<ApplicationUser> FindByUsername(string username);

        public Task<List<ApplicationUser>> FindByUsernames(List<string> usernames);

        public Task<List<ApplicationUser>> FindByIds(List<Guid> ids);

        public Task<UserDto> FindByEmailAsync(string email);

        public Task<UserDto> FindByEmailOrUsernameAsync(string email, string username);

        public Task<UserDto> FindByPhoneAsync(string phone);


        public Task<List<UserDto>> FindAllByRoleNameAsync(string roleName, bool activeOnly = false);

        public Task<List<UserDto>> FindAllByRoleIdAsync(Guid roleId, bool activeOnly = false);

        public string GenerateToken(Guid id);

        public bool ValidateToken(Guid id, string token);

        public Task<UserDto> CreateAsync(UserCreateDto input);
        public Task<UserManagementInsertResultDto> InsertAsync(UserDto user);

        public string CreatePassword(int length);

        public Task<bool> IsUserInRoleAsync(Guid userId, string roleName);

        public bool IsCurrentUserInRole(params string[] roles);
        public bool IsCurrentUserRoleMatch(params string[] roles) => !string.IsNullOrEmpty(CurrentUserRoleName) &&
                    roles.Any(r => r.Contains(CurrentUserRoleName));

        public List<string> CurrentUserRoles { get; }

        public ApplicationUser CurrentUser { get; }
        public Guid? CurrentUserId { get; }
        public Guid? CurrentUserRoleId { get; }
        public string CurrentUserRoleName { get; }

        public Task<List<UserDto>> GetUsersInRoles(List<string> roleNames);

        public Task<string> GetUserNamesInRole(string roleName);

        public string FindCurrentUserFullName();

        public Task<bool> ChangeUserStatus(Guid id);

        public Task<ReturnResult> ValidateUser(UserCreateOrUpdateDtoBase input, Guid? id = null);

        public Task<string> GenerateMobileToken(Guid id);

        public Task<bool> ValidateMobileToken(Guid id, string token);

        public Task<string> GenerateEmailToken(Guid id);

        public Task<bool> ValidateEmailToken(Guid id, string token);

        public Task<string> GeneratePasswordResetTokenAsync(UserDto user);

        public Task<IdentityResult> ResetPasswordAsync(UserDto user, string token, string password);


        public Task<bool> ValidateADUser(string userName, string password);

        public ADUserCreateDto FindUser(string userName);

        public ADUserCreateDto GetUserFromActiveDirectory(string userName);

        public IEnumerable<SelectListItem> GetApplicationRoles();

        public Task<List<Guid>> GetUserRolesAsync();

        public Task<bool> RemoveUserFromRole(Guid userId, Guid roleId);
        public Task<bool> RemoveUserFromRole(Guid userId, string roleName);

        Task<IdentityResult> AddRoleClaim(Guid userId, Guid roleId);

        public bool IsAdmin { get; }
        public string GetClaimValueByKey(string Key);
        public List<string> CurrentPortId { get; }
        public List<string> CurrentPortName { get; }
    }
}
