using EcoTracker.Application.Interfaces;
using EcoTracker.Application.ViewModels.Leaderboard;
using EcoTracker.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTracker.API.Controllers
{
    public class LeaderboardController(ILeaderboardAppService leaderboardAppService) : ApiController
    {
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<LeaderboardViewModel>> GetLeaderboardAsync() => await leaderboardAppService.GetLeaderboardAsync();

        [HttpGet("summary")]
        [Authorize]
        public async Task<IEnumerable<LeaderboardViewModel>> GetSummaryAsync() => await leaderboardAppService.GetLeaderboardAsync();

    }
}
