namespace EcoTracker.Application.ViewModels
{
    public class UpdatePickUpScheduleViewModel
    {
        public required string Street { get; set; }
        public required string Number { get; set; }
        public required string Neighborhood { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string PostalCode { get; set; }
        public required string WasteType { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}