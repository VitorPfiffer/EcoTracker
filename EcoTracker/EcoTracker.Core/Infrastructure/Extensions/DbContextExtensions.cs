using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Core.Infrastructure.Extensions
{
    public static class DbContextExtensions
    {
        public static async Task BeginTransactionAsync(this DbContext context)
        {
            await context.Database.BeginTransactionAsync();
        }

        public static async Task<bool> CommitTransactionAsync(this DbContext context)
        {
            try
            {
                await context.Database.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task AddOrUpdateAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : Entity
        {
            var entityDb = await context.GetByIdAsync<TEntity>(entity.Id);

            if (entityDb is not null)
            {
                context.Entry(entityDb).CurrentValues.SetValues(entity);
                context.Entry(entityDb).State = EntityState.Modified;
                return;
            }

            await context.GetDbSet<TEntity>().AddAsync(entity);
        }

        public static async Task UpdateAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : Entity
        {
            var entityDb = await context.GetByIdAsync<TEntity>(entity.Id);

            if (context is null)
                return;

            context.Entry(entityDb).CurrentValues.SetValues(entity);
            context.Entry(entityDb).State = EntityState.Modified;
        }

        public static async Task UpdateRangeAsync<TEntity>(this DbContext context, params TEntity[] entities) where TEntity : Entity
         => context.GetDbSet<TEntity>().UpdateRange(entities);

        public static async Task AddAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : Entity
            => await context.GetDbSet<TEntity>().AddAsync(entity);

        public static async Task AddRangeAsync<TEntity>(this DbContext context, params TEntity[] entities) where TEntity : Entity
         => context.GetDbSet<TEntity>().AddRangeAsync(entities);

        public static async Task DeleteAsync<TEntity>(this DbContext context, TEntity entity) where TEntity : Entity
            => await Task.Run(() => context.GetDbSet<TEntity>().Remove(entity));

        public static async Task DeleteRangeAsync<TEntity>(this DbContext context, params TEntity[] entities) where TEntity : Entity
            => context.GetDbSet<TEntity>().RemoveRange(entities);

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this DbContext context, Guid id) where TEntity : Entity
            => await context.GetDbSet<TEntity>().FindAsync(id);

        public static async Task<IEnumerable<TEntity>> ListByIdsAsync<TEntity>(this DbContext context, params Guid[] ids) where TEntity : Entity
            => await context.GetDbSet<TEntity>().Where(x => ids.Contains(x.Id)).ToListAsync();

        public static DbSet<TSet> GetDbSet<TSet>(this DbContext context) where TSet : class
            => context.Set<TSet>();
    }
}
