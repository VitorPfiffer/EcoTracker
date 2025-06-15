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
    public class WasteDisposalServiceApp : ApplicationService, IWasteDisposalServiceApp
    {
        private readonly IWasteDisposalDomainService _WasteDisposalDomainService;
        private readonly IEcoTrackerUnitOfWork _unitOfWork;

        public WasteDisposalServiceApp(INotificationManager notificationManager, IMapper mapper, IWasteDisposalDomainService WasteDisposalDomainService, IEcoTrackerUnitOfWork unitOfWork) : base(notificationManager, mapper)
        {
            _WasteDisposalDomainService = WasteDisposalDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<WasteDisposalViewModel?> GetByIdAsync(Guid id)
        {
            var WasteDisposal = await _WasteDisposalDomainService.GetByIdAsync(id);

            var viewModel = _mapper.Map<WasteDisposalViewModel>(WasteDisposal);

            return viewModel;
        }

        public async Task AddAsync(AddWasteDisposalViewModel model)
        {
            var WasteDisposal = _mapper.Map<WasteDisposal>(model);

            await _WasteDisposalDomainService.AddAsync(WasteDisposal);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateWasteDisposalViewModel model)
        {
            var WasteDisposal = _mapper.Map<WasteDisposal>(model);
            WasteDisposal.SetId(id);

            await _WasteDisposalDomainService.UpdateAsync(WasteDisposal);
        }
        public async Task DeleteAsync(Guid Id)
        {
            var WasteDisposal = await _WasteDisposalDomainService.GetByIdAsync(Id);

            if (WasteDisposal == null) return;
            await _WasteDisposalDomainService.DeleteAsync(WasteDisposal);

        }
        public async Task<IEnumerable<WasteDisposalViewModel>> GetPagedAsync(PagedQuery queryParameters)
        {
            var WasteDisposalList = await _WasteDisposalDomainService.GetPagedAsync(queryParameters);

            return _mapper.Map<IEnumerable<WasteDisposalViewModel>>(WasteDisposalList);
        }
    }
}