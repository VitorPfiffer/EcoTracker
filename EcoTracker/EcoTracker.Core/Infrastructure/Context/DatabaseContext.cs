using Microsoft.EntityFrameworkCore;

namespace EcoTracker.Core.Infrastructure.Context
{
    public abstract class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var contextType = this.GetType();

            var infraAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.ToLower().Contains("infrastructure") && x.DefinedTypes.Any(z => z == contextType));

            modelBuilder.ApplyConfigurationsFromAssembly(infraAssembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
