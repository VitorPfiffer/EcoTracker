using EcoTracker.Core.Infrastructure.Context;
using EcoTracker.Core.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EcoTracker.Core.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IHostApplicationBuilder AddDbContext<TContext, TUnitOfWork>(this IHostApplicationBuilder appBuilder,
            string configurationName,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
                where TContext : DatabaseContext
                where TUnitOfWork : UnitOfWork<TContext>
        {
            appBuilder.Services.AddDbContext<TContext, TUnitOfWork>(appBuilder.Configuration, configurationName, lifetime);

            return appBuilder;
        }

        public static IServiceCollection AddDbContext<TContext, TUnitOfWork>(this IServiceCollection services,
            IConfiguration configuration,
            string configurationName,
            ServiceLifetime lifetime = ServiceLifetime.Scoped)
                where TContext : DatabaseContext
                where TUnitOfWork : UnitOfWork<TContext>
        {
            var interfaceType = typeof(TUnitOfWork).GetInterfaces().FirstOrDefault(x => x.GetInterface(nameof(IUnitOfWork), true) != null);

            if (interfaceType is null)
                throw new Exception("UnitOfWork dont inherit from IUnitOfWork.");

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    services.AddSingleton(interfaceType, typeof(TUnitOfWork));

                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(interfaceType, typeof(TUnitOfWork));

                    break;
                default:
                    services.AddScoped(interfaceType, typeof(TUnitOfWork));

                    break;
            }

            services.AddDbContext<TContext>(opt =>
            {
                opt.UseOracle(configuration["ConnectionStrings:" + configurationName]);
            }, lifetime);

            return services;
        }
    }
}
