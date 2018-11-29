using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.ApiResult;
using Core.Common.Dto.Api;
using Core.Domain.Abstract.Repositories.Base;
using Core.Domain.Dto;
using Core.Domain.Entity.Base;
using Core.Infrastructure.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.DataAccess.Repositories.Base
{
    public abstract class BaseRepository<T, Type> : IBaseRepository<T, Type> where T : BaseEntity<Type>, new()
    {
        protected DbSet<T> _dbSet;
        private readonly IUnitOfWork _uow;

        public BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _dbSet = _uow.Set<T>();
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return entity;
        }

        public virtual async Task InsertRangeAsync(List<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
            await _uow.SaveChangesAsync();
        }

        public virtual async Task<Type> DeleteAsync(Type id)
        {
            var entity = await _dbSet.FindAsync(id);
            entity.IsRemoved = true;
            _uow.SaveChanges();
            return entity.Id;
        }

        public async Task<ApiResultList<T>> GetListAsync(PaginationDto pagination)
        {
            var query = _dbSet
                .AsNoTracking()
                .OrderByDescending(c => c.Id)
                .AsQueryable();

            var result = await query.Skip(pagination.PageIndex)
                .Take(pagination.PageSize)
                .ToListAsync();

            return new ApiResultList<T>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public virtual async Task<Type> UpdateAsync(T entity)
        {
            var model = await FindAsync(entity.Id);
            if (model == null)
            {
                return default(Type); //equal null
            }
            _uow.Entry(model).CurrentValues.SetValues(entity);
            await _uow.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<Type> UpdateRangeAsync(List<T> items)
        {
            _dbSet.UpdateRange(items);
            await _uow.SaveChangesAsync();
            return default(Type);
        }

        public virtual async Task<T> FindAsync(Type id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual T Find(Type id)
        {
            return _dbSet.Find(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public virtual IQueryable<T> GetDbSet(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> localEntities = _dbSet.AsQueryable();
            if (expression != null)
            {
                localEntities = localEntities.Where(expression);
            }
            return localEntities;
        }

        public virtual DbSet<T> GetDbSet()
        {
            return _dbSet;
        }

        public virtual async Task<ApiResultList<T>> GetListAsync()
        {
            var query = _dbSet.AsNoTracking().OrderByDescending(c => c.Id).AsQueryable();

            var result = await query.ToListAsync();

            return new ApiResultList<T>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = result.Count()
            };
        }
    }
}
