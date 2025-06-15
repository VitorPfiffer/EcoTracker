using EcoTracker.Core.Api.Pagination;

namespace EcoTracker.Core.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);
        Task AddOrUpdateAsync(TEntity entity);
        Task AddRangeAsync(params TEntity[] entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(params TEntity[] entities);
        Task DeleteAsync(TEntity entity);
        Task DeleteRangeAsync(params TEntity[] entities);
        Task<TEntity?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<List<TEntity>> ListAsync(bool includeDeleted = false);
        Task<List<TEntity>> ListByIdsAsync(bool includeDeleted = false, params Guid[] ids);
        Task<IEnumerable<TEntity>> GetPagedAsync(PagedQuery queryParameters);
    }
}
