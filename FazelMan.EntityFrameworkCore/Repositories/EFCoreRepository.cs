using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FazelMan.Application.Services;
using FazelMan.Domain.Entities;
using FazelMan.Domain.Uow;
using FazelMan.Extentions;
using Microsoft.EntityFrameworkCore;

namespace FazelMan.EntityFrameworkCore.Repositories
{
    public class EFCoreRepository<TEntity, TType> : IRepository<TEntity, TType> where TEntity : Entity<TType>, new()
    {
        private readonly IDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public EFCoreRepository(IDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity Find(TType id)
        {
            return _entities.Find(id);
        }

        public async Task<TEntity> FindAsync(TType id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _entities.AnyAsync(expression);
        }

        public TEntity Insert(TEntity entity, bool isSave = true)
        {
            _entities.Add(entity);
            if (isSave) _context.SaveChanges();
            return entity;
        }

        public void Insert(IEnumerable<TEntity> entity, bool isSave = true)
        {
            _entities.AddRange(entity);
            if (isSave) _context.SaveChanges();
        }

        public async Task<TEntity> InsertAsync(TEntity entity, bool isSave = true)
        {
            await _entities.AddAsync(entity);
            if (isSave) await _context.SaveChangesAsync();
            return entity;
        }

        public async Task InsertAsync(IEnumerable<TEntity> entity, bool isSave = true)
        {
            await _entities.AddRangeAsync(entity);
            if (isSave) await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity, bool isSave = true)
        {
            var table = _entities.Find(entity.Id);
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

        public void Delete(TType id, bool isSave = true)
        {
            var table = _entities.Find(id);
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

        public void Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            foreach (var item in entities)
            {
                var table = _entities.Find(item.Id);
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

        public async Task DeleteAsync(TType id, bool isSave = true)
        {
            var table = await _entities.FindAsync(id);
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

        public TType Update(TEntity entity, bool isSave = true)
        {
            var model = Find(entity.Id);
            if (model == null)
            {
                return default(TType);
            }
            _context.Entry(model).CurrentValues.SetValues(entity);
            if (isSave) _context.SaveChanges();

            return entity.Id;
        }

        public void Update(IEnumerable<TEntity> entities, bool isSave = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.UpdateRange(entities);
            if (isSave) _context.SaveChanges();
        }

        public async Task<TType> UpdateAsync(TEntity entity, bool isSave = true)
        {
            var model = await FindAsync(entity.Id);
            if (model == null)
            {
                return default(TType);
            }
            _context.Entry(model).CurrentValues.SetValues(entity);
            if (isSave) await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, bool isSave = true)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            _entities.UpdateRange(entities);
            if (isSave) await _context.SaveChangesAsync();
        }


        public IQueryable<TEntity> Table()
        {
            return _entities;
        }

        public IQueryable<TEntity> TableNoTracking()
        {
            return _entities.AsNoTracking();
        }

        public IPagedList<TEntity> GetAll(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _entities.AsNoTracking();
            return new PagedList<TEntity>(query, pageIndex, pageSize);
        }
    }
}
