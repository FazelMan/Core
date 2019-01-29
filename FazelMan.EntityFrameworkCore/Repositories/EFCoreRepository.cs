using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FazelMan.Application.Services;
using FazelMan.Application.Services.ApiResult;
using FazelMan.Domain.Entities;
using FazelMan.Domain.Uow;
using FazelMan.Dto.Api;
using FazelMan.Extentions;
using Microsoft.EntityFrameworkCore;

namespace FazelMan.EntityFrameworkCore.Repositories
{
    public class EFCoreRepository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : Entity<TType>, new()
    {
        private readonly IDbContext _context;
        private DbSet<TEntity> _entities;

        public EFCoreRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool isSave = true)
        {
            await _entities.AddAsync(entity);
            if (isSave) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task InsertRangeAsync(List<TEntity> entity, bool isSave = true)
        {
            await _entities.AddRangeAsync(entity);
            if (isSave) await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity, bool isSave = true)
        {
            var table = await _entities.FindAsync(entity.Id);
            PropertyInfo property = table.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsRemoved");
            if (property != null)
            {
                SetValueWithReflectionExtention.SetValue(table, "IsRemoved", true);
                if (isSave) _context.SaveChanges();
            }
            else
            {
                _entities.Remove(table);
                if (isSave) _context.SaveChanges();
            }
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities, bool isSave = true)
        {
            foreach (var item in entities)
            {
                var table = await _entities.FindAsync(item.Id);
                PropertyInfo property = table.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsRemoved");
                if (property != null)
                {
                    SetValueWithReflectionExtention.SetValue(table, "IsRemoved", true);
                    if (isSave) _context.SaveChanges();
                }
                else
                {
                    _entities.Remove(table);
                    if (isSave) _context.SaveChanges();
                }
            }
        }

        public async Task<ApiResultList<TEntity>> GetListAsync(PaginationDto pagination)
        {
            var query = _entities.AsNoTracking();

            var result = await query
            .Skip((pagination.PageIndex - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();

            return new ApiResultList<TEntity>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public async Task<ApiResultList<TEntity>> GetListAsync()
        {
            var query = _entities.AsNoTracking();

            var result = await query.ToListAsync();

            return new ApiResultList<TEntity>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public async Task<TType> UpdateAsync(TEntity entity, bool isSave = true)
        {
            var model = await FindAsync(entity.Id);
            if (model == null)
            {
                return default(TType); //equal null
            }
            _context.Entry(model).CurrentValues.SetValues(entity);
            if (isSave) await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<TType> UpdateAsync(IEnumerable<TEntity> entities, bool isSave = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.UpdateRange(entities);
            if (isSave) await _context.SaveChangesAsync();
            return default(TType);
        }

        public async Task<TEntity> FindAsync(TType id)
        {
            return await _entities.FindAsync(id);
        }

        public TEntity Find(TType id)
        {
            return _entities.Find(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }

        public IQueryable<TEntity> Table()
        {
            return _entities;
        }

        public IQueryable<TEntity> TableNoTracking()
        {
            return _entities.AsNoTracking();
        }

        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            if (propertySelectors == null || propertySelectors.Length <= 0)
            {
                return Table();
            }

            var query = Table();

            foreach (var propertySelector in propertySelectors)
            {
                query = query.Include(propertySelector);
            }

            return query;
        }

        public async Task<List<TEntity>> GetAllListAsync()
        {
            return await Table().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table().Where(predicate).ToListAsync();
        }

    }
}
