using Framework.Identity.Data.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Identity.Data.Services.Interfaces
{
    public interface IRoleAppService
    {
        public Task<RoleDto> GetAsync(Guid id);

        public Task<RoleDto> FindByRoleNameAsync(string roleName);

        public RoleSearchDto GetList(RoleSearchDto model);
        public Task<IEnumerable<SelectListItem>> List();

        public Task<bool> DeleteAsync(Guid id);

        public Task<List<string>> GetRoleNamesByIds(List<Guid> ids);
        public Task<List<RoleDto>> GetAllAsync();
        public Task<RoleManagementInsertResultDto> InsertAsync(RoleDto role);
        public Task<RoleDto> GetRoleByIdAsync(Guid id);
        public Task<Guid> UpdateAsync(RoleDto input);

    }
}
