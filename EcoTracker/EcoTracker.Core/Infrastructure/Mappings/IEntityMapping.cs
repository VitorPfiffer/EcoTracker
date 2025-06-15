using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Core.Infrastructure.Mappings
{
    public interface IEntityMapping
    {
        void Apply(ModelBuilder modelBuilder);
    }
}
