using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.Swagger.Data
{
    public class IOC
    {
        private readonly IServiceProvider _serviceProvider;

        public IOC(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public TService GetService<TService>() where TService : class => _serviceProvider.GetService<TService>();
    }
}
