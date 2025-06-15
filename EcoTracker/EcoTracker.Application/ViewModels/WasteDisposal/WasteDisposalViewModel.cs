namespace EcoTracker.Application.ViewModels
{
    public class WasteDisposalViewModel
    {
        public required string WasteType { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}