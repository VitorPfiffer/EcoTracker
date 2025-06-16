using EcoTracker.Core.Api.Pagination;
using EcoTracker.Domain.Entities;

namespace EcoTracker.Domain.Interfaces.Repositories
{
    public interface IPickUpScheduleRepository
    {
        Task AddAsync(PickUpSchedule entity);
        Task<PickUpSchedule?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task UpdateAsync(PickUpSchedule entity);
        Task DeleteAsync(PickUpSchedule entity);
        Task<IEnumerable<PickUpSchedule>> GetPagedAsync(PagedQuery queryParameters);
    }
}