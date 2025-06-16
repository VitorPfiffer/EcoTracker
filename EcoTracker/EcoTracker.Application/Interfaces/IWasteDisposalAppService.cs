using EcoTracker.Application.ViewModels;
using EcoTracker.Application.ViewModels.WasteDisposal;
using EcoTracker.Core.Api.Pagination;

namespace EcoTracker.Application.Interfaces
{
    public interface IWasteDisposalServiceApp
    {
        Task AddAsync(AddWasteDisposalViewModel model);
        Task<WasteDisposalViewModel?> GetByIdAsync(Guid id);
        Task<WasteDisposalViewModel?> GetByUserIdAsync(Guid userId);
        Task DeleteAsync(Guid Id);
        Task UpdateAsync(Guid id, UpdateWasteDisposalViewModel model);
        Task<IEnumerable<WasteDisposalViewModel>> GetPagedAsync(PagedQuery queryParameters);
        Task<MonthlyWasteReportViewModel> GetTotalWasteByMonthAsync(int year, int month);
    }
}