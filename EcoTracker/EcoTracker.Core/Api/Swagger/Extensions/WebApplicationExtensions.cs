using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace EcoTracker.Core.Swagger.Extensions
{
    public static class WebApplicationExtensions
    {
        private static string RoutePrefix = "api/documentation";

        public static WebApplication UseSwaggerDocumentation(this WebApplication app)
        {
            RoutePrefix = FormatRoute(app.Services.GetRequiredService<IConfiguration>()["Swagger:Url"]) ?? RoutePrefix;

            app.MapSwagger(pattern: "/" + RoutePrefix + "/{documentName}/swagger.{json|yaml}");
            app.UseSwaggerUI(action =>
            {
                action.EnableFilter();

                action.InjectJavascript("https://code.jquery.com/jquery-3.7.1.min.js");
                action.InjectJavascript("/swagger/custom.js");
                action.InjectStylesheet("/swagger/custom.css");

                action.RoutePrefix = RoutePrefix;

                SetSwaggerEndpointByApiVersion(action, app);
            });

            return app;
        }

        private static void SetSwaggerEndpointByApiVersion(SwaggerUIOptions option, WebApplication app)
        {
            try
            {
                if (app?.DescribeApiVersions()?.Count > 1)
                    foreach (var description in app.DescribeApiVersions())
                        SetSwaggerEndpoint(option, description.GroupName);
                else
                    SetSwaggerEndpoint(option, "v1");
            }
            catch (Exception)
            {
                SetSwaggerEndpoint(option, "v1");
            }
        }

        private static void SetSwaggerEndpoint(SwaggerUIOptions option, string version) =>
            option.SwaggerEndpoint($"/{RoutePrefix}/{version}/swagger.json", version.ToUpperInvariant());

        private static string FormatRoute(string route)
        {
            if (string.IsNullOrWhiteSpace(route)) return null;

            if (route.StartsWith("/"))
                route = new string(route.Skip(1).ToArray());

            if (route.EndsWith("/"))
                route = route.Substring(0, route.Length - 1);

            return route;
        }
    }
}
