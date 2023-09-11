using Framework.Core.Angular.Dto;
using Framework.Core.AutoMapper;
using Framework.Core.Extensions;
using Framework.Core.Globalization;
using Framework.Core.SharedServices.Services;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Framework.Identity.Data.Repositories;
using Framework.Identity.Data.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Framework.Identity.Data.Services
{
    public class RoleAppService : IRoleAppService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly RoleRepository _roleRepository;
        private readonly AppSettingsService _appSettingsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleAppService(
            RoleManager<ApplicationRole> roleManager,
           RoleRepository roleRepository, AppSettingsService appSettingsService,
           IHttpContextAccessor httpContextAccessor)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _appSettingsService = appSettingsService;
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentUserName => _httpContextAccessor?.HttpContext?.User?.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();

        public async Task<IEnumerable<SelectListItem>> List()
        {
            return await _roleRepository.TableNoTracking
              .Select(s => new SelectListItem
              {
                  Text = CultureHelper.IsArabic ? s.DisplayNameAr : s.DisplayNameEn,
                  Value = s.Name,
              }).ToListAsync();
        }
        public async Task<RoleDto> GetAsync(Guid id)
        {
            var result = await _roleManager.FindByIdAsync(id.ToString());
            return result.MapTo<RoleDto>();

        }
        public async Task<List<RoleDto>> GetAllAsync()
        {
            var result = await _roleRepository.TableNoTracking.ToListAsync();
            return result.MapTo<List<RoleDto>>();
        }
        public async Task<List<SelectListItem<Guid>>> GetRolesList()
        {
            var roles = await _roleRepository.TableNoTracking.Select(p => new SelectListItem<Guid>
            {
                Value = p.Id,
                NameAr = p.DisplayNameAr,
                NameEn = p.DisplayNameEn,
            }).ToListAsync();

            return roles;
        }
        public async Task<string> GetRoleNameByRoleId(Guid roleId)
        {
            return await _roleRepository.TableNoTracking.Where(q => q.Id == roleId).Select(q => CultureHelper.IsArabic ? q.DisplayNameAr : q.DisplayNameEn).FirstOrDefaultAsync();
        }
        public async Task<string> GetRoleByRoleId(Guid roleId)
        {
            return await _roleRepository.TableNoTracking.Where(q => q.Id == roleId).Select(q => q.Name).FirstOrDefaultAsync();
        }
        public async Task<RoleDto> FindByRoleNameAsync(string roleName)
        {
            try
            {
                var result = await _roleManager.FindByNameAsync(roleName);
                return result.MapTo<RoleDto>();
            }

            catch (Exception e)
            {
                throw;
            }
        }

        public RoleSearchDto GetList(RoleSearchDto model)
        {

            var filters = new List<Expression<Func<ApplicationRole, bool>>>();


            if (!model.Name.IsNullOrEmpty())
            {
                Expression<Func<ApplicationRole, bool>>
                    filter = r => r.DisplayNameAr.ToLower().Contains(model.Name) || r.DisplayNameEn.ToLower().Contains(model.Name);
                filters.Add(filter);
            }

            Func<IQueryable<ApplicationRole>, IOrderedQueryable<ApplicationRole>> orderBy;
            if (model.IsDescending)
            {
                orderBy = a => a.OrderByDescending(b => b.Name);
            }
            else
            {
                orderBy = a => a.OrderBy(b => b.Name);
            }

            model.PageSize = _appSettingsService.DefaultPagerPageSize;

            var result = _roleRepository.SearchWithFilters
                (
                model.PageNumber,
                model.IsExport ? _appSettingsService.ExportNoOfItems : model.PageSize.Value,
                orderBy,
                filters
                );

            model.Items =
                new StaticPagedList<ApplicationRole>(
                    result,
                    result.PageNumber,
                    result.PageSize,
                    result.TotalItemCount);

            return model;


        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _roleManager.FindByIdAsync(id.ToString());
            if (user != null)
                return await _roleRepository.DeleteAsync(u => u.Id == id, true);
            else
                return false;
        }

        public async Task<List<string>> GetRoleNamesByIds(List<Guid> ids)
        {
            var roles = await _roleRepository.TableNoTracking.Where(s => ids.Contains(s.Id)).Select(s => CultureHelper.IsArabic ? s.DisplayNameAr : s.DisplayNameEn).ToListAsync();
            return roles;

        }
        public async Task<RoleManagementInsertResultDto> InsertAsync(RoleDto role)
        {
            RoleManagementInsertResultDto objResult = new RoleManagementInsertResultDto();
            var NormalizedName = _roleManager.NormalizeKey(role.Name);
            var UserObj = await _roleManager.FindByNameAsync(NormalizedName);
            if (UserObj != null)
            {
                objResult.InsertedId = Guid.Empty;
                objResult.VerificationMSG = Resources.SharedResources.UserAlreadyExist;
                return objResult;
            }
            role.NormalizedName = NormalizedName;
            var entity = role.MapTo<ApplicationRole>();
            entity.CreatedBy = CurrentUserName;
            var resultRole = await _roleRepository.InsertAsync(entity, true);
            objResult.InsertedId = resultRole.Id;

            return objResult;
        }
        public async Task<RoleDto> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            var result = role.MapTo<RoleDto>();
            return result;
        }
        public async Task<Guid> UpdateAsync(RoleDto input)
        {
            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            if (!role.Id.ToString().IsNotNullOrEmpty())
            {
                return Guid.Empty;
            }
            role.Name = input.Name;
            role.DisplayNameAr = input.DisplayNameAr;
            role.DisplayNameEn = input.DisplayNameEn;

            await _roleManager.UpdateAsync(role);

            return role.Id;
        }
    }
}
