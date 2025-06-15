using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.AutoMapper
{
    public static class Injection
    {
        public static IServiceCollection AddAutoMapper<TProfile>(this IServiceCollection services) where TProfile : Profile
        {
            services.AddAutoMapper([typeof(TProfile)]);

            return services;
        }
    }
}
