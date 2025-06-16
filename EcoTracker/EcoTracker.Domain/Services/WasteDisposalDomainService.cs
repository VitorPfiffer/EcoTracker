using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Domain.Services;
using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Domain.Interfaces.Services;

namespace EcoTracker.Domain.Services
{
    public sealed class WasteDisposalDomainService : DomainService, IWasteDisposalDomainService
    {
        private readonly IWasteDisposalRepository _WasteDisposalRepository;

        public WasteDisposalDomainService(INotificationManager notificationManager, IValidatorManager validationManager, IWasteDisposalRepository WasteDisposalRepository) : base(notificationManager, validationManager)
        {
            _WasteDisposalRepository = WasteDisposalRepository;
        }

        public async Task AddAsync(WasteDisposal entity)
        {

            await _WasteDisposalRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(WasteDisposal entity)
        {
            await _WasteDisposalRepository.DeleteAsync(entity);
        }

        public async Task<WasteDisposal?> GetByUserIdAsync(Guid userId, bool includeDeleted = false)
        {
            var WasteDisposal = await _WasteDisposalRepository.GetByUserIdAsync(userId, includeDeleted);
            if (WasteDisposal == null)
                NotifyError("WasteDisposal_not_found");

            return WasteDisposal;
        }

        public async Task<WasteDisposal?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var WasteDisposal = await _WasteDisposalRepository.GetByIdAsync(id, includeDeleted);
            if (WasteDisposal == null)
                NotifyError("WasteDisposal_not_found");

            return WasteDisposal;
        }

        public async Task UpdateAsync(WasteDisposal entity)
        {
            await _WasteDisposalRepository.UpdateAsync(entity);
        }
        public async Task<IEnumerable<WasteDisposal>> GetPagedAsync(PagedQuery queryParameters)
        {
            return await _WasteDisposalRepository.GetPagedAsync(queryParameters);
        }
    }
}