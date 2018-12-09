using Core.Common.Dto.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Common.ApiResult;
using Core.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace Core.Domain.Abstract.Repositories.Base
{
    public interface IBaseRepository<T, Type> where T : class
    {
        Task<T> InsertAsync(T entity);
        Task InsertRangeAsync(List<T> entity);
        Task<ApiResultList<T>> GetListAsync(PaginationDto pagination);
        Task<ApiResultList<T>> GetListAsync();
        Task<T> FindAsync(Type id);
        T Find(Type id);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<Type> DeleteAsync(Type id);
        Task DeleteRangeAsync(List<T> list);
        Task<Type> UpdateAsync(T entity);
        Task<Type> UpdateRangeAsync(List<T> items);
        IQueryable<T> GetDbSet(Expression<Func<T, bool>> expression);
        DbSet<T> GetDbSet();
    }
}
