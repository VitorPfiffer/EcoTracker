using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;

namespace EcoTracker.Application.Interfaces
{
    public interface IPickUpScheduleServiceApp
    {
        Task AddAsync(AddPickUpScheduleViewModel model);
        Task<PickUpScheduleViewModel?> GetByIdAsync(Guid id);
        Task DeleteAsync(Guid Id);
        Task UpdateAsync(Guid id, UpdatePickUpScheduleViewModel model);
        Task<IEnumerable<PickUpScheduleViewModel>> GetPagedAsync(PagedQuery queryParameters);

    }
}