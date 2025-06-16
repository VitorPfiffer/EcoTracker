namespace EcoTracker.Application.ViewModels.Leaderboard
{
    public sealed class LeaderboardSummaryViewModel
    {
        public double TotalDiscardKg { get; set; }
        public int UserCount { get; set; }
        public required string MostDiscardedType { get; set; }
        public double AverageDiscardPerUser { get; set; }
    }
}
