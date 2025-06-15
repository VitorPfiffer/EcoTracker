using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Settings.Configuration;

namespace EcoTracker.Core.Api.Serilog
{
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Serilog and HttpLogging configuration.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;


            builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
            {
                loggerConfiguration
                .ReadFrom.Configuration(configuration, new ConfigurationReaderOptions());
            });

            //Configura o log automatico para o RequestBody e ResponseBody para os endpoints dessa aplicação
            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.ResponseBody;
            });
        }
        //Configure UseHttpLogging.
        public static WebApplication UseSerilog(this WebApplication app)
        {
            app.UseHttpLogging();

            return app;
        }
    }
}
