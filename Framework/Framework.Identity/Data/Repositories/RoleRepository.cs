using Framework.Core.Data.Repositories;
using Framework.Identity.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Identity.Data.Repositories
{
    public class RoleRepository : RepositoryBase<AppIdentityDbContext, ApplicationRole>
    {
        public RoleRepository(AppIdentityDbContext dbContext)
            : base(dbContext)
        {
        }


        public virtual async Task<ApplicationRole> FindByNameAsync(string name)
        {
            return await TableNoTracking.FirstOrDefaultAsync(r => r.Name == name);
        }

        public ApplicationRole GetById(Guid id)
        {
            return TableNoTracking.FirstOrDefaultAsync(r => r.Id == id).Result;
        }

        public async Task<ApplicationRole> GetByIdAsync(Guid id)
        {
            return await TableNoTracking.FirstOrDefaultAsync(r => r.Id == id);
        }

        public virtual async Task<List<ApplicationRole>> GetListAsync()
        {
            return await TableNoTracking.ToListAsync();
        }

    }
}