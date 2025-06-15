using EcoTracker.Core.FluentValidator.ValidatorManager;
using EcoTracker.Core.NotificationManager;

namespace EcoTracker.Core.Domain.Services
{
    public abstract class DomainService : IDomainService
    {
        protected readonly INotificationManager NotificationManager;

        protected readonly IValidatorManager _validatorManager;

        public DomainService(INotificationManager notificationManager, IValidatorManager validatorManager)
        {
            NotificationManager = notificationManager;
            _validatorManager = validatorManager;
        }

        //Precisa passar o CallerFilePath aqui para que o notifyError do NotificationManager pegue o nome da classe que herda de DomainService
        //se não vai pegar somente o nome dessa classe "DomainService"
        public void NotifyError(string code) => NotificationManager.NotifyError(code);
        public void NotifyError(string code, string details) => NotificationManager.NotifyError(code, details);
        //Precisa passar o CallerFilePath aqui para que o notifyError do NotificationManager pegue o nome da classe que herda de DomainService
        //se não vai pegar somente o nome dessa classe "DomainService"
        public void NotifyErrors(IEnumerable<string> messages) => NotificationManager.NotifyErrors(messages);

        public bool HasErrors() => NotificationManager.Has(Enums.NotificationType.Error);

        public async Task<bool> ValidateAsync<TValidator>(object value)
        {
            return await _validatorManager.ValidateAsync<TValidator>(value);
        }

    }
}
