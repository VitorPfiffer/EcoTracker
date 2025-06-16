namespace EcoTracker.Application.ViewModels.Leaderboard
{
    public class LeaderboardViewModel
    {
        public Guid UserId { get; set; }
        public required string Username { get; set; }
        public decimal TotalWasteRecycled { get; set; }
        public required string Unit { get; set; }
        public required List<string> WasteTypes { get; set; }
    }
}
