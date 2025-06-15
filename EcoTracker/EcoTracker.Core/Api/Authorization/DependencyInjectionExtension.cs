using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EcoTracker.Core.Api.Authorization
{
    public static class DependencyInjectionExtension
    {
        public static IHostApplicationBuilder AddJwtAuthentication(this IHostApplicationBuilder appBuilder,
    string configurationName)
        {
            appBuilder.Services.AddJwtAuthentication(appBuilder.Configuration, configurationName);

            return appBuilder;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration,
        string configurationName)
        {
            services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>(configurationName)!);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

            services.AddAuthorization();

            return services;
        }
    }
}
