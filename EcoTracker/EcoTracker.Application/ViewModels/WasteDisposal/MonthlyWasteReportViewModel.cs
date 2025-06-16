namespace EcoTracker.Application.ViewModels.WasteDisposal
{

    public class MonthlyWasteReportViewModel
    {
        public required string Month { get; set; }
        public int Year { get; set; }
        public required IEnumerable<WasteTypeTotal> WasteTypes { get; set; }
    }

    public class WasteTypeTotal
    {
        public required string Type { get; set; }
        public decimal TotalKg { get; set; }
    }
}
