﻿using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Framework.Core.Data.Repositories
{
    public interface IRepositoryBase<TContext, TEntity>
        where TContext : IBaseDbContext
        where TEntity : class
    {
        IQueryable<TEntity> Table { get; }

        IQueryable<TEntity> TableNoTracking { get; }


        //================================================================
        //============================ INSERT ============================
        //================================================================
        [NotNull]
        TEntity Insert([NotNull] TEntity entity, bool autoSave = false);
        [NotNull]
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false);

        Task InsertRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false);
        void InsertRange(IEnumerable<TEntity> entities, bool autoSave = false);


        //================================================================
        //============================ UPDATE ============================
        //================================================================

        [NotNull]
        TEntity Update([NotNull] TEntity entity, bool autoSave = false);
        [NotNull]
        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false);
        void UpdateRange(IEnumerable<TEntity> entities, bool autoSave = false);

        Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false);
        //================================================================
        //============================ DELETE ============================
        //================================================================

        void Delete([NotNull] TEntity entity, bool autoSave = false);
        void Delete([NotNull] Expression<Func<TEntity, bool>> predicate, bool autoSave = false);
        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false);
        Task<bool> DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate, bool autoSave = false);

        void DeleteRange(IEnumerable<TEntity> entities, bool autoSave = false);

        long GetCount();

        Task<long> GetCountAsync();

        List<TEntity> SearchWithFilters(
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
                params Expression<Func<TEntity, object>>[] includes);

        PagedList<TEntity> SearchWithFilters(
            int pageNumber,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
            params Expression<Func<TEntity, object>>[] includes);

        PagedList<TResult> SearchAndSelectWithFilters<TResult>(
            int pageNumber,
            int pageSize,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            Expression<Func<TEntity, TResult>> selectors,
            IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
           params Expression<Func<TEntity, object>>[] includes
        );
        List<TResult> SearchAndSelectWithFilters<TResult>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
        Expression<Func<TEntity, TResult>> selectors,
        IEnumerable<Expression<Func<TEntity, bool>>> filters = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
       params Expression<Func<TEntity, object>>[] includes
    );

        void Delete(object id, bool autoSave = false);
        Task DeleteAsync(object id, bool autoSave = false);

        [NotNull]
        TEntity GetById(object id);
        [NotNull]
        Task<TEntity> GetByIdAsync(object id);

    }

}
