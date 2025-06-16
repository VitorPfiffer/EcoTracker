using EcoTracker.Application.ViewModels.Leaderboard;

namespace EcoTracker.Application.Interfaces
{
    public interface ILeaderboardAppService
    {
        Task<IEnumerable<LeaderboardViewModel>> GetLeaderboardAsync();
    }
}
