using EcoTracker.Core.Swagger.ActionFilter;
using EcoTracker.Core.Swagger.CodeInjection;
using EcoTracker.Core.Swagger.Controllers;
using EcoTracker.Core.Swagger.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EcoTracker.Core.Swagger.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSingleton<IOC>();
            services.AddSingleton<CssManager>();
            services.AddSingleton<JavascriptManager>();

            services.AddEndpointsApiExplorer();

            services.AddControllers().AddApplicationPart(typeof(SwaggerController).Assembly);
            services.AddSwaggerGen(opt =>
            {
                opt.OperationFilter<SwaggerDocFilter>();
                opt.EnableAnnotations();
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerOptions>();
        }
    }
}