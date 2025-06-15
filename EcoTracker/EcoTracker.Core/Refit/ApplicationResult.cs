using Newtonsoft.Json;


namespace EcoTracker.Core.Refit
{
    public class ApplicationResult<T>
    {
        public int Id { get; set; }
        public string Version { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public T Result { get; set; }
        [JsonProperty("messages")]
        public IEnumerable<Notification> Notifications { get; set; }
        [JsonIgnore]
        public bool HasNotifications => Notifications.Any();
    }
    public class Notification
    {
        public int MessageType { get; set; }

        public string Code { get; set; }
        public string Message { get; set; }

        public int ApplicationMessageType { get; set; }
        public int NotificationMessageType { get; set; }

        public bool RequiredConfirmation { get; set; }

    }
}
