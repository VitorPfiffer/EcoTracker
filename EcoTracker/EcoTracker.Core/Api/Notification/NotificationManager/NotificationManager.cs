using System.Collections.ObjectModel;
using System.Reflection;
using System.Resources;
using EcoTracker.Core.Enums;


namespace EcoTracker.Core.NotificationManager
{
    internal sealed class NotificationManager : ITranslatableNotificationManager
    {
        private readonly List<Data.Notification> _notifications = new List<Data.Notification>();
        private readonly ResourceManager _errorMessagesResource;
        private readonly ResourceManager _businessValidationMessagesResource;
        private readonly ResourceManager _informationMessagesResource;

        public NotificationManager(string errorMessagesResourceName, string businessValidationMessagesResourceName, string informationMessagesResourceName)
        {
            Assembly mainAssembly = Assembly.GetEntryAssembly();

            if (!string.IsNullOrWhiteSpace(errorMessagesResourceName))
            {
                _errorMessagesResource = new ResourceManager(mainAssembly.GetName().Name + "." + errorMessagesResourceName, mainAssembly);
            }

            if (!string.IsNullOrWhiteSpace(businessValidationMessagesResourceName))
            {
                _businessValidationMessagesResource = new ResourceManager(mainAssembly.GetName().Name + "." + businessValidationMessagesResourceName, mainAssembly);
            }

            if (!string.IsNullOrWhiteSpace(informationMessagesResourceName))
            {
                _informationMessagesResource = new ResourceManager(mainAssembly.GetName().Name + "." + errorMessagesResourceName, mainAssembly);
            }

            _notifications = new List<Data.Notification>();
        }

        public void Reset()
        {
            _notifications.Clear();
        }

        public void TranslateMessages()
        {
            var translatedNotifications = new List<Data.Notification>();

            foreach (Data.Notification notificationToTranslate in _notifications)
            {
                var translatedMessage = "-";

                try
                {
                    switch (notificationToTranslate.Type)
                    {
                        case NotificationType.Error:
                            translatedMessage = _errorMessagesResource.GetString(notificationToTranslate.Code);
                            break;

                        case NotificationType.BusinessValidation:
                            translatedMessage = _businessValidationMessagesResource.GetString(notificationToTranslate.Code);

                            break;

                        case NotificationType.Information:
                            translatedMessage = _informationMessagesResource.GetString(notificationToTranslate.Code);

                            break;
                    }
                }
                catch { }

                translatedMessage ??= notificationToTranslate.Message;
                translatedNotifications.Add(
                    new Data.Notification(notificationToTranslate.Code,
                        translatedMessage,
                        notificationToTranslate.Details,
                        notificationToTranslate.Type));

            }

            Reset();

            _notifications.AddRange(translatedNotifications);
        }

        public void NotifyError(string code)
        {
            try
            {
                var message = _errorMessagesResource.GetString(code);

                if (string.IsNullOrWhiteSpace(message))
                    throw new Exception("Error code don't exists in your resource file for culture " + Thread.CurrentThread.CurrentUICulture.Name);

                AddError(code, message, string.Empty);
            }
            catch
            {
                throw;
            }
        }
        public void NotifyError(string code, string details)
        {
            try
            {
                var message = _errorMessagesResource.GetString(code);

                if (string.IsNullOrWhiteSpace(message))
                    throw new Exception("Error code don't exists in your resource file for culture " + Thread.CurrentThread.CurrentUICulture.Name);

                AddError(code, message, details);
            }
            catch
            {
                throw;
            }
        }

        public void NotifyErrors(IEnumerable<string> errorMessages)
        {
            foreach (var error in errorMessages)
            {
                AddError(error);
            }
        }

        public void AddError(string code, string details) =>
            AddError(code, string.Empty, details);

        public void AddError(string message) =>
            AddError(string.Empty, message, string.Empty);

        public void AddError(string code, string message, string details)
        {
            var notification = new Data.Notification(code, message, details, NotificationType.Error);
            _notifications.Add(notification);
        }


        public void AddBusinessValidation(string code) =>
            AddBusinessValidation(code, string.Empty);

        public void AddBusinessValidation(string code, string details) =>
            AddBusinessValidation(code, string.Empty, details);

        public void AddBusinessValidation(string code, string message, string details) =>
            _notifications.Add(new Data.Notification(code, message, details, NotificationType.BusinessValidation));


        public void AddInformation(string code) =>
            AddInformation(code, string.Empty);

        public void AddInformation(string code, string details) =>
            AddInformation(code, string.Empty, details);
        public void AddInformation(string code, string message, string details) =>
            _notifications.Add(new Data.Notification(code, message, details, NotificationType.Information));


        public ReadOnlyCollection<Data.Notification> Get(NotificationType type) => new ReadOnlyCollection<Data.Notification>(_notifications.Where(x => x.Type == type).ToList());

        public ReadOnlyCollection<Data.Notification> GetAll() => new ReadOnlyCollection<Data.Notification>(_notifications);

        public bool Has(NotificationType type) => _notifications.Any(x => x.Type == type);

    }
}
