using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FazelMan.Core.Application.Services;
using FazelMan.Core.Application.Services.ApiResult;
using FazelMan.Core.Domain.Entities;
using FazelMan.Core.Domain.Uow;
using FazelMan.Core.Dto.Api;
using Microsoft.EntityFrameworkCore;

namespace FazelMan.Core.EntityFrameworkCore.EntityFrameworkCore.Repositories
{
    public abstract class BaseRepository<T, Type> : IBaseRepository<T, Type> where T : BaseEntity<Type>, new()
    {
        public DbSet<T> Table;
        private readonly IUnitOfWork _uow;

        protected BaseRepository(IUnitOfWork uow)
        {
            _uow = uow;
            Table = _uow.Set<T>();
        }

        public virtual async Task<T> InsertAsync(T entity, bool isSave = true)
        {
            await Table.AddAsync(entity);
            if (isSave) await _uow.SaveChangesAsync();
            return entity;
        }

        public virtual async Task InsertRangeAsync(List<T> entity, bool isSave = true)
        {
            await Table.AddRangeAsync(entity);
            if (isSave) await _uow.SaveChangesAsync();
        }

        public virtual async Task<Type> DeleteAsync(Type id, bool isSave = true)
        {
            var entity = await Table.FindAsync(id);
            entity.IsRemoved = true;
            if (isSave) _uow.SaveChanges();
            return entity.Id;
        }

        public virtual async Task DeleteRangeAsync(List<T> list, bool isSave = true)
        {
            foreach (var item in list)
            {
                var entity = await Table.FindAsync(item.Id);
                entity.IsRemoved = true;
                _uow.SaveChanges();
            }
        }

        public virtual async Task<ApiResultList<T>> GetListAsync(PaginationDto pagination)
        {
            var query = Table.AsNoTracking();

            var result = await query
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

            return new ApiResultList<T>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public virtual async Task<ApiResultList<T>> GetListAsync()
        {
            var query = Table.AsNoTracking();

            var result = await query.ToListAsync();

            return new ApiResultList<T>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public virtual async Task<Type> UpdateAsync(T entity, bool isSave = true)
        {
            var model = await FindAsync(entity.Id);
            if (model == null)
            {
                return default(Type); //equal null
            }
            _uow.Entry(model).CurrentValues.SetValues(entity);
            if (isSave) await _uow.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task<Type> UpdateRangeAsync(List<T> items, bool isSave = true)
        {
            Table.UpdateRange(items);
            if (isSave) await _uow.SaveChangesAsync();
            return default(Type);
        }

        public virtual async Task<T> FindAsync(Type id)
        {
            return await Table.FindAsync(id);
        }

        public virtual T Find(Type id)
        {
            return Table.Find(id);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Table;
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] propertySelectors)
        {
            if (propertySelectors == null || propertySelectors.Length <= 0)
            {
                return GetAll();
            }

            var query = GetAll();

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }

        public virtual async Task<List<T>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public virtual async Task<List<T>> GetAllListAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

    }
}
