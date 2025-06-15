using EcoTracker.Core.FluentValidator.ValidatorManager;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.FluentValidator
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Scan the project for classes that inherit from AbstractValidator and automatically add them as scope.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.Scan(s => s
                .FromApplicationDependencies(a => a.FullName.StartsWith("EcoTracker"))
                .AddClasses(classes => classes.AssignableTo(typeof(AbstractValidator<>)))
                .AsImplementedInterfaces()
            .WithScopedLifetime()
            );

            services.AddScoped<IValidatorManager, ValidatorManager.ValidatorManager>();
            return services;
        }
    }
}
