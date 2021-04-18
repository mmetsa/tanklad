using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Contracts.Domain.Base;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class BaseRepository<TEntity>: BaseRepository<TEntity, Guid>, IBaseRepository<TEntity>
        where TEntity : class, IDomainEntityId
    {
        public BaseRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IDomainEntityId<TKey> 
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext RepoDbContext;
        protected readonly DbSet<TEntity> RepoDbSet;
        public BaseRepository(DbContext dbContext)
        {
            RepoDbContext = dbContext;
            RepoDbSet = dbContext.Set<TEntity>();
        }
        
        private IQueryable<TEntity> CreateQuery(TKey? userId = default, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (userId != null && typeof(TEntity).IsAssignableFrom(typeof(IDomainAppUserId<TKey>)))
            {
                query = query.Where(e => ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
            }

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(TKey? userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            return await query.FirstOrDefaultAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return RepoDbSet.Add(entity).Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            return RepoDbSet.Update(entity).Entity;
        }

        public virtual TEntity Remove(TEntity entity, TKey? userId = default)
        {
            // TODO: load entity from db, check that userId inside entity is correct.
            if (userId != null && !((IDomainAppUserId<TKey>) entity).AppUserId.Equals(userId))
            {
                throw new AuthenticationException("Bad user id inside deleted entity");
            }
            var query = CreateQuery(userId, false);
            return RepoDbSet.Remove(entity).Entity;
        }

        public virtual async Task<TEntity> RemoveAsync(TKey id, TKey? userId = default, bool noTracking = true)
        {
            var entity = await FirstOrDefaultAsync(id, userId, noTracking);
            if (entity == null)
            {
                throw new NullReferenceException($"Entity with id {id} not found.");
            }
            return Remove(entity, userId);
        }

        public virtual async Task<bool> ExistsAsync(TKey id, TKey? userId = default)
        {
            if (userId == null || userId.Equals(default))
            {
                return await RepoDbSet.AnyAsync(e => e.Id.Equals(id));   
            }
            if (!typeof(IDomainAppUserId<TKey>).IsAssignableFrom(typeof(TEntity)))
                throw new AuthenticationException(
                    $"Entity does not implement required interface: {typeof(IDomainAppUserId<TKey>).Name} for AppUserId check");
            return await RepoDbSet.AnyAsync(e =>
                e.Id.Equals(id) && ((IDomainAppUserId<TKey>) e).AppUserId.Equals(userId));
        }
    }
}