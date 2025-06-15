using AutoMapper;
using EcoTracker.Core.NotificationManager;

namespace EcoTracker.Core.Application
{
    public class ApplicationService : IApplicationService
    {
        protected readonly IMapper _mapper;

        protected readonly INotificationManager _notificationManager;

        public ApplicationService(INotificationManager notificationManager, IMapper mapper)
        {
            _notificationManager = notificationManager;
            _mapper = mapper;
        }

        public void NotifyError(string code) => _notificationManager.NotifyError(code);

        public bool HasErrors() => _notificationManager.Has(Enums.NotificationType.Error);
    }
}
