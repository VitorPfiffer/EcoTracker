using EcoTracker.Core.Api.Notification.NotificationManager;
using EcoTracker.Core.NotificationManager;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTracker.Core.Extensions
{
    public static class NotificationInjection
    {

        public static IServiceCollection AddNotification(this IServiceCollection services,
        Action<NotificationManagerOptions> configureOptions)
        {


            var options = new NotificationManagerOptions();
            configureOptions(options);

            SetNotification(services, options);
            return services;
        }

        public static IServiceCollection AddNotification(this IServiceCollection services, string ErrorMessagesResourceName)
        {


            var options = new NotificationManagerOptions();
            options.ErrorMessagesResourceName = ErrorMessagesResourceName;

            SetNotification(services, options);
            return services;
        }

        public static IServiceCollection SetNotification(this IServiceCollection services,
       NotificationManagerOptions options)
        {

            // Registrar o NotificationManager e usar as opções configuradas
            services.AddScoped<INotificationManager, NotificationManager.NotificationManager>(provider =>
            {
                return new NotificationManager.NotificationManager(
                    options.ErrorMessagesResourceName,
                    options.BusinessValidationMessagesResourceName,
                    options.InformationMessagesResourceName
                );
            });

            return services;
        }
    }
}
