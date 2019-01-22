using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FazelMan.Application.Services;
using FazelMan.Application.Services.ApiResult;
using FazelMan.Domain.Entities;
using FazelMan.Domain.Repositories;
using FazelMan.Domain.Uow;
using FazelMan.Dto.Api;
using FazelMan.Extentions;
using Microsoft.EntityFrameworkCore;

namespace FazelMan.EntityFrameworkCore.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity, TPrimaryKey> : FazelManRepositoryBase<TEntity, TPrimaryKey>

        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        public virtual TDbContext Context => _dbContextProvider.GetDbContext();
        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public virtual DbConnection Connection
        {
            get
            {
                var connection = Context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                return connection;
            }
        }

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        private readonly IUnitOfWork _uow;

        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool isSave = true)
        {
            await Table.AddAsync(entity);
            if (isSave) await _uow.SaveChangesAsync();
            return entity;
        }

        public override async Task InsertRangeAsync(List<TEntity> entity, bool isSave = true)
        {
            await Table.AddRangeAsync(entity);
            if (isSave) await _uow.SaveChangesAsync();
        }

        public override async Task DeleteAsync(TPrimaryKey id, bool isSave = true)
        {
            var table = await Table.FindAsync(id);
            PropertyInfo property = table.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsRemoved");
            if (property != null)
            {
                SetValueWithReflectionExtention.SetValue(table, "IsRemoved", true);
                if (isSave) _uow.SaveChanges();
            }
            else
            {
                Table.Remove(table);
                if (isSave) _uow.SaveChanges();
            }
        }

        public override void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public override async Task DeleteRangeAsync(List<TEntity> list, bool isSave = true)
        {
            foreach (var item in list)
            {
                var table = await Table.FindAsync(item.Id);
                PropertyInfo property = table.GetType().GetProperties().FirstOrDefault(x => x.Name == "IsRemoved");
                if (property != null)
                {
                    SetValueWithReflectionExtention.SetValue(table, "IsRemoved", true);
                    if (isSave) _uow.SaveChanges();
                }
                else
                {
                    Table.Remove(table);
                    if (isSave) _uow.SaveChanges();
                }
            }
        }

        public override async Task<ApiResultList<TEntity>> GetListAsync(PaginationDto pagination)
        {
            var query = Table.AsNoTracking();

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

        public override async Task<ApiResultList<TEntity>> GetListAsync()
        {
            var query = Table.AsNoTracking();

            var result = await query.ToListAsync();

            return new ApiResultList<TEntity>
            {
                Result = result,
                FilteredCount = result.Count,
                TotalCount = query.Count()
            };
        }

        public override async Task<TPrimaryKey> UpdateAsync(TEntity entity, bool isSave = true)
        {
            var model = await FindAsync(entity.Id);
            if (model == null)
            {
                return default(TPrimaryKey); //equal null
            }
            _uow.Entry(model).CurrentValues.SetValues(entity);
            if (isSave) await _uow.SaveChangesAsync();

            return entity.Id;
        }

        public override async Task<TPrimaryKey> UpdateRangeAsync(List<TEntity> items, bool isSave = true)
        {
            Table.UpdateRange(items);
            if (isSave) await _uow.SaveChangesAsync();
            return default(TPrimaryKey);
        }

        public override async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            return await Table.FindAsync(id);
        }

        public override TEntity Find(TPrimaryKey id)
        {
            return Table.Find(id);
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await Table.AnyAsync(expression);
        }

        public override void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Table;
        }

        public override IQueryable<TEntity> GetAllNoTracking()
        {
            return Table.AsNoTracking();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
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

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            return await GetAll().ToListAsync();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await GetAll().Where(predicate).ToListAsync();
        }

    }
}
