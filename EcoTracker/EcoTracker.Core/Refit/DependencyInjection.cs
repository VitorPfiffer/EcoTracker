using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Refit;

namespace EcoTracker.Core.Refit
{
    public static class DependencyInjection
    {
        public static IHostApplicationBuilder AddApiClient<TClient, TTokenProvider>(this IHostApplicationBuilder builder, string apiName)
          where TClient : class
          where TTokenProvider : TokenProvider
        {
            builder.Services.AddApiClient<TClient, TTokenProvider>(builder.Configuration, apiName);

            return builder;
        }

        public static IHostApplicationBuilder AddApiClient<TClient>(this IHostApplicationBuilder builder, string apiName)
            where TClient : class
        {
            builder.Services.AddApiClient<TClient>(builder.Configuration, apiName);

            return builder;
        }

        public static IServiceCollection AddApiClient<TClient, TTokenProvider>(this IServiceCollection services, IConfiguration configuration, string apiName)
       where TClient : class
       where TTokenProvider : TokenProvider
        {
            services.AddHttpContextAccessor();
            services.AddScoped<TTokenProvider>();

            var jsonSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore  // Ignora loops de referência
            };

            var settings = new RefitSettings()
            {

                ContentSerializer = new NewtonsoftJsonContentSerializer(jsonSettings),
            };
            services.AddScoped<LoggingHandler>();

            services.AddRefitClient<TClient>(settings)
                    .AddHttpMessageHandler<TTokenProvider>()
                    .AddHttpMessageHandler<LoggingHandler>()
                    .ConfigureHttpClient(client =>
                    {
                        client.Timeout = TimeSpan.Parse(configuration[$"Apis:{apiName}:Timeout"]!);
                        client.BaseAddress = new Uri(configuration[$"Apis:{apiName}:BaseUrl"]!);
                    });

            return services;
        }

        public static IServiceCollection AddApiClient<TClient>(this IServiceCollection services, IConfiguration configuration, string apiName)
            where TClient : class
        {
            services.AddRefitClient<TClient>()
                     .AddHttpMessageHandler<LoggingHandler>()
                    .ConfigureHttpClient(client =>
                    {
                        client.BaseAddress = new Uri(configuration[$"{apiName}:BaseUrl"]);
                    });

            return services;
        }

        public static WebApplication UseApiClient(this WebApplication app)
        {
            IoC.Provider = app.Services;

            return app;
        }

        public static WebApplication UseIoC(this WebApplication app)
        {
            IoC.Provider = app.Services;

            return app;
        }
    }
}
