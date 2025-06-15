using EcoTracker.Core.Infrastructure.Mappings;
using EcoTracker.Domain.Entities;
using EcoTracker.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTracker.Infrastructure.MapConfigs
{
    public sealed class UserMapConfig : EntityMapping<EcoTrackerContext, User>
    {
        protected override string GetTableName() => "Users";

        public override void ConfigureMapping(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
        }
    }
}
