using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FazelMan.Application.Services.ApiResult;
using FazelMan.Domain.Entities;
using FazelMan.Dto.Api;

namespace FazelMan.Application.Services
{
    public interface IRepository<TEntity, TType> where TEntity : Entity<TType>
    {
        Task<TEntity> InsertAsync(TEntity entity, bool isSave = true);
        Task InsertRangeAsync(List<TEntity> entity, bool isSave = true);
        Task<ApiResultList<TEntity>> GetListAsync(PaginationDto pagination);
        Task<ApiResultList<TEntity>> GetListAsync();
        TEntity Find(TType id);
        Task<TEntity> FindAsync(TType id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task DeleteAsync(TType id, bool isSave = true);
        Task DeleteRangeAsync(List<TEntity> list, bool isSave = true);
        Task<TType> UpdateAsync(TEntity entity, bool isSave = true);
        Task<TType> UpdateRangeAsync(List<TEntity> items, bool isSave = true);
        IQueryable<TEntity> Table();
        IQueryable<TEntity> TableNoTracking();
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);
        Task<List<TEntity>> GetAllListAsync();
        Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
