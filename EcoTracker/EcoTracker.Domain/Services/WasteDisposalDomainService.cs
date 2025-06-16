using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Domain.Services;
using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Validators;

namespace EcoTracker.Domain.Services
{
    public sealed class WasteDisposalDomainService : DomainService, IWasteDisposalDomainService
    {
        private readonly IWasteDisposalRepository _wasteDisposalRepository;

        public WasteDisposalDomainService(INotificationManager notificationManager, IValidatorManager validationManager, IWasteDisposalRepository WasteDisposalRepository) : base(notificationManager, validationManager)
        {
            _wasteDisposalRepository = WasteDisposalRepository;
        }

        public async Task AddAsync(WasteDisposal entity)
        {
            var isValid = await _validatorManager.ValidateAsync<AddWasteDisposalValidator>(entity);

            if (!isValid)
                return;
            await _wasteDisposalRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(WasteDisposal entity)
        {
            await _wasteDisposalRepository.DeleteAsync(entity);
        }

        public async Task<WasteDisposal?> GetByUserIdAsync(Guid userId, bool includeDeleted = false)
        {
            var WasteDisposal = await _wasteDisposalRepository.GetByUserIdAsync(userId, includeDeleted);
            if (WasteDisposal == null)
                NotifyError("WasteDisposal_not_found");

            return WasteDisposal;
        }

        public async Task<WasteDisposal?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var WasteDisposal = await _wasteDisposalRepository.GetByIdAsync(id, includeDeleted);
            if (WasteDisposal == null)
                NotifyError("WasteDisposal_not_found");

            return WasteDisposal;
        }

        public async Task UpdateAsync(WasteDisposal entity)
        {
            var isValid = await _validatorManager.ValidateAsync<UpdateWasteDisposalValidator>(entity);

            if (!isValid)
                return;
            await _wasteDisposalRepository.UpdateAsync(entity);
        }
        public async Task<IEnumerable<WasteDisposal>> GetPagedAsync(PagedQuery queryParameters)
        {
            return await _wasteDisposalRepository.GetPagedAsync(queryParameters);
        }


        public async Task<IEnumerable<WasteDisposal>> GetAllAsync()
        {
            return await _wasteDisposalRepository.GetAllAsync();
        }

        public async Task<IEnumerable<WasteDisposal>> GetByMonthAsync(int year, int month)
        {
            return await _wasteDisposalRepository.GetByMonthAsync(year, month);
        }
    }
}