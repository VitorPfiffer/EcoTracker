using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Domain.Services;
using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Repositories;
using EcoTracker.Domain.Interfaces.Services;

namespace EcoTracker.Domain.Services
{
    public sealed class PickUpScheduleDomainService : DomainService, IPickUpScheduleDomainService
    {
        private readonly IPickUpScheduleRepository _PickUpScheduleRepository;

        public PickUpScheduleDomainService(INotificationManager notificationManager, IValidatorManager validationManager, IPickUpScheduleRepository PickUpScheduleRepository) : base(notificationManager, validationManager)
        {
            _PickUpScheduleRepository = PickUpScheduleRepository;
        }

        public async Task AddAsync(PickUpSchedule entity)
        {

            await _PickUpScheduleRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(PickUpSchedule entity)
        {
            await _PickUpScheduleRepository.DeleteAsync(entity);
        }

        public async Task<PickUpSchedule?> GetByIdAsync(Guid id, bool includeDeleted = false)
        {
            var PickUpSchedule = await _PickUpScheduleRepository.GetByIdAsync(id, includeDeleted);
            if (PickUpSchedule == null)
                NotifyError("PickUpSchedule_not_found");

            return PickUpSchedule;
        }

        public async Task UpdateAsync(PickUpSchedule entity)
        {
            await _PickUpScheduleRepository.UpdateAsync(entity);
        }
        public async Task<IEnumerable<PickUpSchedule>> GetPagedAsync(PagedQuery queryParameters)
        {
            return await _PickUpScheduleRepository.GetPagedAsync(queryParameters);
        }
    }
}