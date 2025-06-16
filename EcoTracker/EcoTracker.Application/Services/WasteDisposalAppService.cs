using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels;
using EcoTracker.Application.ViewModels.WasteDisposal;
using EcoTracker.Core.Api.Pagination;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Entities;
using EcoTracker.Domain.Interfaces.Services;
using EcoTracker.Domain.Interfaces.UnitOfWork;
using Microsoft.Extensions.Logging;
using System.Globalization;

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

            var wasteDisposalDb = await _wasteDisposalDomainService.GetByIdAsync(id);

            if (wasteDisposalDb == null)
                return;

            var wasteDisposal = _mapper.Map<WasteDisposal>(model);
            wasteDisposal.SetId(id);

            wasteDisposal = _mapper.Map(wasteDisposal, wasteDisposalDb);

            await _wasteDisposalDomainService.UpdateAsync(wasteDisposal);

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

        public async Task<MonthlyWasteReportViewModel> GetTotalWasteByMonthAsync(int year, int month)
        {
            var allWaste = await _wasteDisposalDomainService.GetByMonthAsync(year, month);

            if (!allWaste.Any())
            {
                return new MonthlyWasteReportViewModel
                {
                    Month = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(month),
                    Year = year,
                    WasteTypes = []
                };
            }

            var wasteByType = allWaste
                .GroupBy(w => w.WasteType)
                .Select(g => new WasteTypeTotal
                {
                    Type = g.Key,
                    TotalKg = g.Sum(w => w.Quantity)
                })
                .OrderByDescending(x => x.TotalKg)
                .ToList();

            return new MonthlyWasteReportViewModel
            {
                Month = new CultureInfo("pt-BR").DateTimeFormat.GetMonthName(month),
                Year = year,
                WasteTypes = wasteByType
            };
        }
    }
}