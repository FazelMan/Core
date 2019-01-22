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
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        Task<TEntity> InsertAsync(TEntity entity, bool isSave = true);
        TEntity Insert(TEntity entity, bool isSave = true);
        Task InsertRangeAsync(List<TEntity> entity, bool isSave = true);
        Task<ApiResultList<TEntity>> GetListAsync(PaginationDto pagination);
        Task<ApiResultList<TEntity>> GetListAsync();
        TEntity Find(TPrimaryKey id);
        Task<TEntity> FindAsync(TPrimaryKey id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(TEntity entity, bool isSave = true);
        Task DeleteRangeAsync(List<TEntity> list, bool isSave = true);
        Task<TPrimaryKey> UpdateAsync(TEntity entity, bool isSave = true);
        TPrimaryKey Update(TEntity entity, bool isSave = true);
        Task<TPrimaryKey> UpdateRangeAsync(List<TEntity> items, bool isSave = true);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllNoTracking();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
