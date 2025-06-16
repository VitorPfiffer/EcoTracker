using EcoTracker.Core.Infrastructure.Mappings;
using EcoTracker.Domain.Entities;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTracker.Infrastructure.MapConfigs
{
    public sealed class WasteDisposalMapConfig : EntityMapping<EcoTrackerContext, WasteDisposal>
    {
        protected override string GetTableName() => "WasteDisposals";

        public override void ConfigureMapping(EntityTypeBuilder<WasteDisposal> builder)
        {
            builder.Property(x => x.WasteType).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Unit).IsRequired();
            builder.Property(x => x.Date).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.UserId).IsRequired();


            builder.HasOne(w => w.User)
                   .WithMany()
                   .HasForeignKey(w => w.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
