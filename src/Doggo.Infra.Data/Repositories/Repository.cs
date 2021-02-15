using Doggo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Doggo.Infra.Data.Repositories
{
    public abstract class Repository<TEntity> : IDisposable where TEntity : class
    {
        protected DoggoCleanContext Db;
        protected DbSet<TEntity> DbSet;

        public Repository(DoggoCleanContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }


        #region Create/Add
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual IEnumerable<TEntity> AddCollectionWithProxy(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Add(entity);
                yield return entity;
            }
        }
        #endregion

        #region Read/Get
        public virtual async Task<TEntity> Find(object primaryKey)
        {
            return await DbSet.FindAsync(primaryKey).ConfigureAwait(false);
        }

        public virtual async Task<bool> Any(Expression<Func<TEntity, bool>> where)
        {
            return await DbSet.AnyAsync(where).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            if (asNoTracking)
                return await DbSet.AsNoTracking().Where(where).SingleOrDefaultAsync().ConfigureAwait(false);

            return await DbSet.Where(where).SingleOrDefaultAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
                return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);

            return await DbSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            if (asNoTracking)
                return await DbSet.AsNoTracking().Where(where).ToListAsync().ConfigureAwait(false);

            return await DbSet.Where(where).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy, bool asNoTracking = true)
        {
            if (asNoTracking)
                return await DbSet.AsNoTracking().Where(where).OrderBy(orderBy).ToListAsync().ConfigureAwait(false);

            return await DbSet.Where(where).OrderBy(orderBy).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, bool asNoTracking = true)
        {
            var databaseCount = await DbSet.CountAsync().ConfigureAwait(false);

            if (asNoTracking)
                return new Tuple<IEnumerable<TEntity>, int>
                (
                    await DbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                    databaseCount
                );

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await DbSet.Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                databaseCount
            );
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, Expression<Func<TEntity, bool>> where, bool asNoTracking = true)
        {
            var databaseCount = await DbSet.CountAsync().ConfigureAwait(false);

            if (asNoTracking)
                return new Tuple<IEnumerable<TEntity>, int>
                (
                    await DbSet.AsNoTracking().Where(where).Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                    databaseCount
                );

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await DbSet.Where(where).Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                databaseCount
            );
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy, bool asNoTracking = true)
        {
            var databaseCount = await DbSet.CountAsync().ConfigureAwait(false);

            if (asNoTracking)
                return new Tuple<IEnumerable<TEntity>, int>
                (
                    await DbSet.AsNoTracking().OrderBy(orderBy).Where(where).Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                    databaseCount
                );

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await DbSet.OrderBy(orderBy).Where(where).Skip(skip).Take(take).ToListAsync().ConfigureAwait(false),
                databaseCount
            );
        }
        #endregion

        #region Update
        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual IEnumerable<TEntity> UpdateCollectionWithProxy(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                DbSet.Update(entity);
                yield return entity;
            }
        }
        #endregion

        #region Delete/Remove
        public virtual void Remove(Func<TEntity, bool> where)
        {
            DbSet.RemoveRange(DbSet.ToList().Where(where));
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }
        #endregion

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
