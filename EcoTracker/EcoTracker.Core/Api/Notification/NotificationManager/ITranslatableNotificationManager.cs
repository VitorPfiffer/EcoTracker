namespace EcoTracker.Core.NotificationManager
{
    public interface ITranslatableNotificationManager : IResetableNotificationManager
    {
        void TranslateMessages();
    }
}
