using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Interfaces.UnitOfWork;

namespace EcoTracker.Application.Services
{
    public class PickUpScheduleServiceApp : ApplicationService, IPickUpScheduleServiceApp
    {
        private readonly IPickUpScheduleDomainService _PickUpScheduleDomainService;
        private readonly IEcoTrackerUnitOfWork _unitOfWork;

        public PickUpScheduleServiceApp(INotificationManager notificationManager, IMapper mapper, IPickUpScheduleDomainService PickUpScheduleDomainService, IEcoTrackerUnitOfWork unitOfWork) : base(notificationManager, mapper)
        {
            _PickUpScheduleDomainService = PickUpScheduleDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PickUpScheduleViewModel?> GetByIdAsync(Guid id)
        {
            var PickUpSchedule = await _PickUpScheduleDomainService.GetByIdAsync(id);

            var viewModel = _mapper.Map<PickUpScheduleViewModel>(PickUpSchedule);

            return viewModel;
        }

        public async Task AddAsync(AddPickUpScheduleViewModel model)
        {
            var PickUpSchedule = _mapper.Map<PickUpSchedule>(model);

            await _PickUpScheduleDomainService.AddAsync(PickUpSchedule);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdatePickUpScheduleViewModel model)
        {
            var PickUpSchedule = _mapper.Map<PickUpSchedule>(model);
            PickUpSchedule.SetId(id);

            await _PickUpScheduleDomainService.UpdateAsync(PickUpSchedule);
        }
        public async Task DeleteAsync(Guid Id)
        {
            var PickUpSchedule = await _PickUpScheduleDomainService.GetByIdAsync(Id);

            if (PickUpSchedule == null) return;
            await _PickUpScheduleDomainService.DeleteAsync(PickUpSchedule);

        }
        public async Task<IEnumerable<PickUpScheduleViewModel>> GetPagedAsync(PagedQuery queryParameters)
        {
            var PickUpScheduleList = await _PickUpScheduleDomainService.GetPagedAsync(queryParameters);

            return _mapper.Map<IEnumerable<PickUpScheduleViewModel>>(PickUpScheduleList);
        }
    }
}