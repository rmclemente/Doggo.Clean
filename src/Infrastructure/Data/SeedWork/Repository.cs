using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Data.SeedWork
{
    public abstract class Repository<TEntity> : IDisposable where TEntity : class
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
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
        public virtual async Task<TEntity> Find(object primaryKey, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(new object[] { primaryKey }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<bool> Any(CancellationToken cancellationToken = default, params Expression<Func<TEntity, bool>>[] where)
        {
            var query = where.Aggregate(DbSet.AsQueryable(), (qry, path) => qry.Where(path));
            return await query.AnyAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.Where(where);

            if (includes is not null && includes.Any())
                query = includes.Aggregate(query, (query, path) => query.Include(path));

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> Get(bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, bool>>[] where)
        {
            var query = where.Aggregate(DbSet.AsQueryable(), (qry, path) => qry.Where(path));

            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> orderBy, bool asc = true, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = DbSet.Where(where);

            if (asNoTracking)
                query = query.AsNoTracking();

            if (includes is not null && includes.Any())
                query = includes.Aggregate(query, (query, path) => query.Include(path));

            if (asc)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await query.Skip(skip).Take(take).ToListAsync(cancellationToken).ConfigureAwait(false),
                await query.CountAsync(cancellationToken).ConfigureAwait(false)
            );
        }

        public virtual async Task<Tuple<IEnumerable<TEntity>, int>> GetAll(int skip, int take, Expression<Func<TEntity, object>> orderBy, bool asc = true, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, bool>>[] where)
        {
            var query = where.Aggregate(DbSet.AsQueryable(), (qry, path) => qry.Where(path));

            if (asNoTracking)
                query = query.AsNoTracking();

            if (asc)
                query = query.OrderBy(orderBy);
            else
                query = query.OrderByDescending(orderBy);

            return new Tuple<IEnumerable<TEntity>, int>
            (
                await query.Skip(skip).Take(take).ToListAsync(cancellationToken).ConfigureAwait(false),
                await query.CountAsync(cancellationToken).ConfigureAwait(false)
            );
        }
        #endregion

        #region Delete/Remove
        public virtual async Task Remove(Expression<Func<TEntity, bool>> where)
        {
            var range = await DbSet.Where(where).ToListAsync();
            DbSet.RemoveRange(range);
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
