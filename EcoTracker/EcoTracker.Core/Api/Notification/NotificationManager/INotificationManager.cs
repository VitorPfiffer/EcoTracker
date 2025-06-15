using System.Collections.ObjectModel;
using EcoTracker.Core.Enums;

namespace EcoTracker.Core.NotificationManager
{
    public interface INotificationManager
    {
        void NotifyError(string code);
        void NotifyError(string code, string details);
        void AddError(string code, string details);
        void AddError(string code, string message, string details);
        void NotifyErrors(IEnumerable<string> errorMessages);
        ReadOnlyCollection<Data.Notification> Get(NotificationType type);
        ReadOnlyCollection<Data.Notification> GetAll();
        bool Has(NotificationType type);
    }
}
