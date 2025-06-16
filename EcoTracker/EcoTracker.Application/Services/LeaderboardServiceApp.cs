using AutoMapper;
using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels.Leaderboard;
using EcoTracker.Core.Application;
using EcoTracker.Core.NotificationManager;
using EcoTracker.Domain.Interfaces.Services;

namespace EcoTracker.Application.Services
{
    public class LeaderboardAppService : ApplicationService, ILeaderboardAppService
    {
        private readonly IWasteDisposalDomainService _wasteDisposalDomainService;
        public LeaderboardAppService(INotificationManager notificationManager, IMapper mapper, IWasteDisposalDomainService wasteDisposalDomainService) : base(notificationManager, mapper)
        {
            _wasteDisposalDomainService = wasteDisposalDomainService;
        }

        public async Task<IEnumerable<LeaderboardViewModel>> GetLeaderboardAsync()
        {
            var allWaste = await _wasteDisposalDomainService.GetAllAsync();


            var leaderboard = allWaste.GroupBy(wd => wd.User)
        .Select(g => new LeaderboardViewModel()
        {
            UserId = g.Key.Id,
            Username = g.Key.Username,
            TotalWasteRecycled = g.Sum(x => x.Quantity),
            WasteTypes = g.Select(wd => wd.WasteType).Distinct().ToList(),
            Unit = "kg"
        }).ToList();


            return leaderboard.OrderByDescending(lb => lb.TotalWasteRecycled);

        }
    }
}
