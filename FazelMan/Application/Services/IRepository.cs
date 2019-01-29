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
        TEntity Find(TType id);
        Task<TEntity> FindAsync(TType id);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

        TEntity Insert(TEntity entity, bool isSave = true);
        void Insert(IEnumerable<TEntity> entity, bool isSave = true);
        Task<TEntity> InsertAsync(TEntity entity, bool isSave = true);
        Task InsertAsync(IEnumerable<TEntity> entity, bool isSave = true);

        void Delete(TEntity entity, bool isSave = true);
        void Delete(TType id, bool isSave = true);
        void Delete(IEnumerable<TEntity> entities, bool isSave = true);
        Task DeleteAsync(TEntity entity, bool isSave = true);
        Task DeleteAsync(TType id, bool isSave = true);
        Task DeleteAsync(IEnumerable<TEntity> entities, bool isSave = true);

        TType Update(TEntity entity, bool isSave = true);
        void Update(IEnumerable<TEntity> entities, bool isSave = true);
        Task<TType> UpdateAsync(TEntity entity, bool isSave = true);
        Task UpdateAsync(IEnumerable<TEntity> entities, bool isSave = true);

        IQueryable<TEntity> Table();
        IQueryable<TEntity> TableNoTracking();

        IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue);
    }
}
