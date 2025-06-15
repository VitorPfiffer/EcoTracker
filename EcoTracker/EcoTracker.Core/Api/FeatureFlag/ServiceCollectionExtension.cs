using Microsoft.Extensions.Hosting;
using Microsoft.FeatureManagement;

namespace EcoTracker.Core.Api.FeatureFlag
{
    public static class BuilderExtensions
    {
        public static IHostApplicationBuilder AddFeatureManagement(this IHostApplicationBuilder appBuilder, string configurationName)
        {
            appBuilder.Services.AddScopedFeatureManagement(appBuilder.Configuration.GetSection(configurationName));
            return appBuilder;
        }
    }
}
