using EcoTracker.Core.Api.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Core.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public DbContext _context { get; }

        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }


        public async Task AddOrUpdateAsync(TEntity entity)
        {
            var entityDb = await GetByIdAsync(entity.Id);

            if (entityDb is not null)
            {
                _context.Entry(entityDb).CurrentValues.SetValues(entity);
                _context.Entry(entityDb).State = EntityState.Modified;
                return;
            }

            await DbSet.AddAsync(entity);
        }

        public async Task AddOrUpdateRangeAsync(params TEntity[] entities)
        {
            var entitiesToAdd = new List<TEntity>();

            var entitiesIds = entities.Select(x => x.Id).ToArray();
            var dbEntities = await DbSet.Where(x => entitiesIds.Contains(x.Id)).ToListAsync();

            foreach (var entity in entities)
            {
                var dbEntity = dbEntities.FirstOrDefault(x => x.Id == entity.Id);

                if (dbEntity == null)
                {
                    entitiesToAdd.Add(entity);
                    continue;
                }

                DbSet.Entry(dbEntity).CurrentValues.SetValues(entity);
                DbSet.Entry(dbEntity).State = EntityState.Modified;


            }

            await DbSet.AddRangeAsync(entitiesToAdd);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var context = await GetByIdAsync(entity.Id);

            if (context is null)
                return;

            _context.Entry(context).CurrentValues.SetValues(entity);
            _context.Entry(context).State = EntityState.Modified;
        }

        public async Task UpdateRangeAsync(params TEntity[] entities)
         => DbSet.UpdateRange(entities);

        public async Task AddAsync(TEntity entity)
            => await DbSet.AddAsync(entity);

        public async Task AddRangeAsync(params TEntity[] entities)
         => await DbSet.AddRangeAsync(entities);

        public async Task DeleteAsync(TEntity entity)
            => await Task.Run(() => DbSet.Remove(entity));

        public async Task DeleteRangeAsync(params TEntity[] entities)
            => DbSet.RemoveRange(entities);

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null || (!includeDeleted && entity.IsDeleted))
                return null;

            return entity;
        }

        public virtual async Task<List<TEntity>> ListByIdsAsync(bool includeDeleted = false, params Guid[] ids)
            => await DbSet.Where(x => (includeDeleted || !x.IsDeleted) && ids.Contains(x.Id)).ToListAsync();

        public virtual async Task<List<TEntity>> ListAsync(bool includeDeleted = false) =>
            await DbSet.Where(x => includeDeleted || !x.IsDeleted).ToListAsync();


        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(PagedQuery queryParameters)
        {
            return await DbSet
                .ApplyFilters(queryParameters)
                .ApplyPaging(queryParameters)
                .ToListAsync();
        }
    }
}
