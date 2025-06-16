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
        private readonly IPickUpScheduleDomainService _pickUpScheduleDomainService;

        private readonly IEcoTrackerUnitOfWork _unitOfWork;

        public PickUpScheduleServiceApp(INotificationManager notificationManager, IMapper mapper, IPickUpScheduleDomainService PickUpScheduleDomainService, IEcoTrackerUnitOfWork unitOfWork) : base(notificationManager, mapper)
        {
            _pickUpScheduleDomainService = PickUpScheduleDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<PickUpScheduleViewModel?> GetByIdAsync(Guid id)
        {
            var pickUpSchedule = await _pickUpScheduleDomainService.GetByIdAsync(id);

            var viewModel = _mapper.Map<PickUpScheduleViewModel>(pickUpSchedule);

            return viewModel;
        }

        public async Task AddAsync(AddPickUpScheduleViewModel model)
        {
            var pickUpSchedule = _mapper.Map<PickUpSchedule>(model);

            await _pickUpScheduleDomainService.AddAsync(pickUpSchedule);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdatePickUpScheduleViewModel model)
        {
            var pickUpSchedule = _mapper.Map<PickUpSchedule>(model);
            pickUpSchedule.SetId(id);

            await _pickUpScheduleDomainService.UpdateAsync(pickUpSchedule);

            await _unitOfWork.CommitAsync();
        }
        public async Task DeleteAsync(Guid Id)
        {
            var pickUpSchedule = await _pickUpScheduleDomainService.GetByIdAsync(Id);

            if (pickUpSchedule == null) return;
            await _pickUpScheduleDomainService.DeleteAsync(pickUpSchedule);

            await _unitOfWork.CommitAsync();

        }
        public async Task<IEnumerable<PickUpScheduleViewModel>> GetPagedAsync(PagedQuery queryParameters)
        {
            var pickUpScheduleList = await _pickUpScheduleDomainService.GetPagedAsync(queryParameters);

            return _mapper.Map<IEnumerable<PickUpScheduleViewModel>>(pickUpScheduleList);
        }
    }
}