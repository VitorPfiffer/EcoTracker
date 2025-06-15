namespace EcoTracker.Core.Infrastructure
{
    public class Entity : IEntity
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public string CreatedBy { get; private set; } = "System";
        public DateTime? UpdatedAt { get; private set; }
        public string UpdatedBy { get; private set; }
        public bool IsDeleted { get; private set; } = false;

        public Entity SetId(Guid id)
        {
            Id = id;
            return this;
        }

        public Entity ChangeLastWrittenBy(string lastWrittendBy)
        {
            if (string.IsNullOrWhiteSpace(UpdatedBy)) CreatedBy = lastWrittendBy;
            else UpdatedBy = lastWrittendBy;

            return this;
        }

        public Entity ChangeUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
            return this;
        }
    }
}
