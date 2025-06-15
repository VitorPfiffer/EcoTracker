using EcoTracker.Core.Infrastructure;

namespace EcoTracker.Domain.Entities
{
    public class WasteDisposal : Entity
    {
        public required string WasteType { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }

        public required User User { get; set; }
    }

}
