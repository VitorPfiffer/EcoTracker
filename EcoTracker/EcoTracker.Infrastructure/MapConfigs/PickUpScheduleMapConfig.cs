using EcoTracker.Core.Infrastructure.Mappings;
using EcoTracker.Domain.Entities;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTracker.Infrastructure.MapConfigs
{
    public sealed class PickUpScheduleMapConfig : EntityMapping<EcoTrackerContext, PickUpSchedule>
    {
        protected override string GetTableName() => "PickUpSchedules";

        public override void ConfigureMapping(EntityTypeBuilder<PickUpSchedule> builder)
        {
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Neighborhood).IsRequired();
            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.PostalCode).IsRequired();
            builder.Property(x => x.WasteType).IsRequired();
            builder.Property(x => x.ScheduledDate).IsRequired();
        }
    }
}
