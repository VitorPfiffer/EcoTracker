using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.Refit
{
    public static class IoC
    {
        public static IServiceProvider Provider { get; set; }

        public static T GetRequiredService<T>() where T : class
        {
            return Provider.GetRequiredService<T>();
        }
        public static object GetRequiredService(Type serviceType)
        {
            return Provider.GetRequiredService(serviceType);
        }
    }
}
