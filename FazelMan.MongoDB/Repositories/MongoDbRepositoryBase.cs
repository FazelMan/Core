using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using FazelMan.Application.Services;
using FazelMan.Application.Services.ApiResult;
using FazelMan.Domain.Entities;
using FazelMan.Domain.Repositories;
using FazelMan.Dto.Api;
using FazelMan.Extentions;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace FazelMan.MongoDb.Repositories
{
    public class MongoDbRepositoryBase<TEntity> : MongoDbRepositoryBase<TEntity, int>, IRepository<TEntity>
      where TEntity : class, IEntity<int>
    {
        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
            : base(databaseProvider)
        {
        }
    }

    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : FazelManRepositoryBase<TEntity, TPrimaryKey>
      where TEntity : class, IEntity<TPrimaryKey>
    {
        public virtual MongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        public virtual MongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        private readonly IMongoDatabaseProvider _databaseProvider;

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }


        public TEntity Insert(TEntity entity, bool isSave = true)
        {
            Collection.Insert(entity);
            return entity;
        }


        public TEntity Find(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> FindAsync(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public override void Delete(TPrimaryKey id)
        {
            var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            Collection.Remove(query);
        }

        public override async Task<TPrimaryKey> UpdateAsync(TEntity entity, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public override TPrimaryKey Update(TEntity entity, bool isSave = true)
        {
            Collection.Save(entity);
            return entity.Id;
        }

        public override async Task<TPrimaryKey> UpdateRangeAsync(List<TEntity> items, bool isSave = true)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public override IQueryable<TEntity> GetAllNoTracking()
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TEntity>> GetAllListAsync()
        {
            throw new NotImplementedException();
        }

        public override async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
