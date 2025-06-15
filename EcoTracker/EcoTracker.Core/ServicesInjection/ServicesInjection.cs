using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.ServicesInjection
{
    public static class ServicesInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Scan(s => s
               .FromApplicationDependencies(a => a.FullName.StartsWith("EcoTracker"))
               .AddClasses().AsMatchingInterface((service, filter) =>
                   filter.Where(i => i.Name.Equals($"I{service.Name}", StringComparison.OrdinalIgnoreCase)))
               .WithScopedLifetime()
            );

            return services;
        }
    }
}
