using EcoTracker.Core.Enums;

namespace EcoTracker.Core.Data
{
    public sealed class Notification
    {
        public string Code { get; private set; }
        public string Message { get; private set; }
        public string Details { get; private set; }
        public NotificationType Type { get; private set; }
        public Notification(string code, string message, string details, NotificationType type)
        {
            this.Code = code;
            this.Message = message;
            this.Details = details;
            this.Type = type;
        }
    }
}
