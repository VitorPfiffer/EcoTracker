using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EcoTracker.Core.Infrastructure.Mappings
{
    public abstract class EntityMapping<TContext, TEntity> : IEntityMapping, IEntityTypeConfiguration<TEntity>
        where TContext : DbContext
        where TEntity : Entity
    {
        public abstract void ConfigureMapping(EntityTypeBuilder<TEntity> builder);

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id).IsRequired().HasConversion(
            v => v.ToByteArray(),
            v => new Guid(v)); ;

            builder.Property(x => x.IsDeleted).IsRequired().HasConversion(
        v => v ? 1 : 0,    // bool → number
        v => v == 1        // number → bool
    )
    .HasColumnType("NUMBER(1)"); ;

            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasDefaultValue(DateTime.UtcNow);

            builder.Property(x => x.UpdatedBy).IsRequired(false);
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.ToTable(this.GetTableName());

            ConfigureMapping(builder);
        }

        protected virtual string GetTableName()
        {
            string defaultTableName = typeof(TEntity).Name;

            return defaultTableName;
        }

        public void Apply(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }
    }
}
