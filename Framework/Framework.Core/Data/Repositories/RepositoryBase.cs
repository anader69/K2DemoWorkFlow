using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Core.Data.Repositories
{
    public class RepositoryBase<TContext, TEntity> : IRepositoryBase<TContext, TEntity>
    where TContext : IBaseDbContext
    where TEntity : class
    {
        protected TContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; }


        public RepositoryBase(TContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<TEntity>();
        }


        public virtual IQueryable<TEntity> Table => DbSet;

        public virtual IQueryable<TEntity> TableNoTracking => DbSet.AsNoTracking();

        public TEntity Insert(TEntity entity, bool autoSave = false)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return savedEntity;
        }

        public void InsertRange(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.AddRange(entities);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            await DbSet.AddRangeAsync(entities);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false)
        {
            var savedEntity = DbSet.Add(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }

            return savedEntity;
        }

        public TEntity Update(TEntity entity, bool autoSave = false)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                DbContext.SaveChanges();
            }

            return updatedEntity;
        }

        public void UpdateRange(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.UpdateRange(entities);
            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.UpdateRange(entities);
            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false)
        {
            DbContext.Attach(entity);

            var updatedEntity = DbContext.Update(entity).Entity;

            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }

            return updatedEntity;
        }

        public void Delete(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = false)
        {
            DbSet.Remove(entity);

            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities, bool autoSave = false)
        {
            DbSet.RemoveRange(entities);

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public long GetCount()
        {
            return DbSet.LongCount();
        }

        public async Task<long> GetCountAsync()
        {
            return await DbSet.LongCountAsync();
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            foreach (var entity in GetQueryable().Where(predicate).ToList())
            {
                Delete(entity, autoSave);
            }

            if (autoSave)
            {
                DbContext.SaveChanges();
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false)
        {
            var entities = await GetQueryable()
                .Where(predicate)
                .ToListAsync();

            foreach (var entity in entities)
            {
                DbSet.Remove(entity);
            }

            if (autoSave)
            {
                await DbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public virtual List<TEntity> SearchWithFilters(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = TableNoTracking;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            query = query.IncludeMultiple(includes);


            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                throw new Exception();
            }

            return query.ToList();
        }

        public virtual PagedList<TEntity> SearchWithFilters(
            int pageNumber,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
            params Expression<Func<TEntity, object>>[] includes)
        {
            var query = TableNoTracking;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            query = query.IncludeMultiple(includes);


            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                throw new Exception();
            }

            return new PagedList<TEntity>(query, pageNumber, pageSize);
        }


        public virtual PagedList<TResult> SearchAndSelectWithFilters<TResult>(
            int pageNumber,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TResult>> selectors,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
          params Expression<Func<TEntity, object>>[] includes
        )
        {
            var query = TableNoTracking;

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }
            if (includes != null)
                query = query.IncludeMultiple(includes);
            if (orderBy != null)
                query = orderBy(query);

            var queryList = query.Select(selectors);

            return new PagedList<TResult>(queryList, pageNumber, pageSize);
        }
        public virtual List<TResult> SearchAndSelectWithFilters<TResult>(
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
    Expression<Func<TEntity, TResult>> selectors,
    IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
  params Expression<Func<TEntity, object>>[] includes
)
        {
            var query = TableNoTracking;

            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }
            if (includes != null)
                query = query.IncludeMultiple(includes);
            if (include != null)
            {
                query = include(query);
            }


            query = orderBy(query);

            var queryList = query.Select(selectors);

            return queryList.ToList();
        }
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual void Delete(object id, bool autoSave = false)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                return;
            }

            Delete(entity, autoSave);
        }

        public virtual async Task DeleteAsync(object id, bool autoSave = false)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave);
        }


    }

}



