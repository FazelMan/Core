using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FazelMan.Application.Services.ApiResult;
using FazelMan.Domain.Entities;
using FazelMan.Dto.Api;

namespace FazelMan.Domain.Repositories
{
    public abstract class FazelManRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        static FazelManRepositoryBase()
        {
           
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Insert(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task InsertRangeAsync(List<TEntity> entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ApiResultList<TEntity>> GetListAsync(PaginationDto pagination)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<ApiResultList<TEntity>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public virtual TEntity Find(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }
        public abstract void Delete(TEntity entity);

        public virtual Task DeleteAsync(TEntity entity, bool isSave = true)
        {
            Delete(entity);
            return Task.FromResult(0);
        }

        public abstract void Delete(TPrimaryKey id);

        public virtual Task DeleteAsync(TPrimaryKey id, bool isSave = true)
        {
            Delete(id);
            return Task.FromResult(0);
        }

        public virtual async Task DeleteRangeAsync(List<TEntity> list, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TPrimaryKey> UpdateAsync(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual TPrimaryKey Update(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TPrimaryKey> UpdateRangeAsync(List<TEntity> items, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public abstract IQueryable<TEntity> GetAll();

        public virtual IQueryable<TEntity> GetAllNoTracking()
        {
            return GetAll();
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync()
        {
            return GetAll().ToList();
        }

        public virtual async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }
    }
}
