using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;

namespace EcoTracker.Core.Domain.Services
{
    /// <summary>
    /// Classe base dedicada para Domains especializados em consumir apis.
    /// </summary>
    public class ApiDomainService : DomainService
    {
        public ApiDomainService(INotificationManager notificationManager, IValidatorManager validationManager) : base(notificationManager, validationManager)
        {
        }
    }
}
