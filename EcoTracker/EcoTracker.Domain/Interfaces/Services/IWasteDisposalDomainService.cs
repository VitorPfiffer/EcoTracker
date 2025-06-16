using EcoTracker.Core.Api.Pagination;
using EcoTracker.Domain.Entities;

namespace EcoTracker.Domain.Interfaces.Services
{
    public interface IWasteDisposalDomainService
    {
        Task AddAsync(WasteDisposal entity);
        Task<WasteDisposal?> GetByIdAsync(Guid id, bool includeDeleted = false);
        Task<WasteDisposal?> GetByUserIdAsync(Guid userId, bool includeDeleted = false);
        Task UpdateAsync(WasteDisposal entity);
        Task DeleteAsync(WasteDisposal entity);
        Task<IEnumerable<WasteDisposal>> GetPagedAsync(PagedQuery queryParameters);
    }
}