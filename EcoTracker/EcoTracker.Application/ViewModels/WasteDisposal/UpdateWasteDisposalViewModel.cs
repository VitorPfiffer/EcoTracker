namespace EcoTracker.Application.ViewModels
{
    public class UpdateWasteDisposalViewModel
    {
        public required string WasteType { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
    }
}