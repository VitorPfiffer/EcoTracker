using Microsoft.AspNetCore.Builder;

namespace EcoTracker.Core.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseWebApi(this IApplicationBuilder applicationBuilder)
        {
            WebApplication webApplication = (WebApplication)applicationBuilder;

            webApplication.MapControllers();
        }
    }
}
