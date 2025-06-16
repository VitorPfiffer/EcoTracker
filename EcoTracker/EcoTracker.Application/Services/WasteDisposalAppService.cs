using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace EcoTracker.Application.Services
{
    public class WasteDisposalServiceApp : ApplicationService, IWasteDisposalServiceApp
    {
        private readonly IWasteDisposalDomainService _wasteDisposalDomainService;
        private readonly IUserDomainService _userDomainService;
        private readonly ILogger<WasteDisposalServiceApp> _logger;
        private readonly IEcoTrackerUnitOfWork _unitOfWork;

        public WasteDisposalServiceApp(INotificationManager notificationManager, IMapper mapper, IWasteDisposalDomainService WasteDisposalDomainService, IEcoTrackerUnitOfWork unitOfWork, IUserDomainService userDomainService, ILogger<WasteDisposalServiceApp> logger) : base(notificationManager, mapper)
        {
            _wasteDisposalDomainService = WasteDisposalDomainService;
            _unitOfWork = unitOfWork;
            _userDomainService = userDomainService;
            _logger = logger;
        }
        public async Task<WasteDisposalViewModel?> GetByUserIdAsync(Guid userId)
        {
            var WasteDisposal = await _wasteDisposalDomainService.GetByUserIdAsync(userId);

            var viewModel = _mapper.Map<WasteDisposalViewModel>(WasteDisposal);

            return viewModel;
        }
        public async Task<WasteDisposalViewModel?> GetByIdAsync(Guid id)
        {
            var WasteDisposal = await _wasteDisposalDomainService.GetByIdAsync(id);

            var viewModel = _mapper.Map<WasteDisposalViewModel>(WasteDisposal);

            return viewModel;
        }

        public async Task AddAsync(AddWasteDisposalViewModel model)
        {
            var wasteDisposal = _mapper.Map<WasteDisposal>(model);

            var user = await _userDomainService.GetByIdAsync(model.UserId);

            if (user == null)
                return;

            await _wasteDisposalDomainService.AddAsync(wasteDisposal);

            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Guid id, UpdateWasteDisposalViewModel model)
        {
            var WasteDisposal = _mapper.Map<WasteDisposal>(model);
            WasteDisposal.SetId(id);


            await _wasteDisposalDomainService.UpdateAsync(WasteDisposal);

            await _unitOfWork.CommitAsync();

        }
        public async Task DeleteAsync(Guid Id)
        {
            var WasteDisposal = await _wasteDisposalDomainService.GetByIdAsync(Id);

            if (WasteDisposal == null) return;
            await _wasteDisposalDomainService.DeleteAsync(WasteDisposal);

            await _unitOfWork.CommitAsync();


        }
        public async Task<IEnumerable<WasteDisposalViewModel>> GetPagedAsync(PagedQuery queryParameters)
        {
            var WasteDisposalList = await _wasteDisposalDomainService.GetPagedAsync(queryParameters);
            return _mapper.Map<IEnumerable<WasteDisposalViewModel>>(WasteDisposalList);
        }
    }
}