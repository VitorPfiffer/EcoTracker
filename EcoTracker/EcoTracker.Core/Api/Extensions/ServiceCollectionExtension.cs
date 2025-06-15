using Asp.Versioning;
using EcoTracker.Core.Api;
using EcoTracker.Core.Api.ActionFilters;
using EcoTracker.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddWebApi(this IServiceCollection serviceCollection, Action<MvcOptions>? configure = null)
        {
            ControllersInfo.HasVersioning = true;

            serviceCollection.AddRouting(x => x.LowercaseUrls = true);

            serviceCollection.AddScoped<ApiResponse>();

            serviceCollection.AddControllers(options =>
                {
                    configure?.Invoke(options);
                    options.Filters.Add<ExceptionHandlerFilter>();
                    options.Filters.Add<ApiResponseNormalizerActionFilter>();
                }
            ).AddNewtonsoftJson();

            serviceCollection.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
