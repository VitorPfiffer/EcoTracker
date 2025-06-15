namespace EcoTracker.Core.Api.Notification.NotificationManager
{
    public class NotificationManagerOptions
    {
        public string ErrorMessagesResourceName { get; set; }
        public string BusinessValidationMessagesResourceName { get; set; } = "";
        public string InformationMessagesResourceName { get; set; } = "";
    }
}
